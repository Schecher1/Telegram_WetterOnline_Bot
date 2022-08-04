using Telegram.Bot;

namespace Telegram_WetterOnline_Bot.Core
{
    public class TelegramBot
    {
        public static TelegramBotClient _client;

        public void SetAPIKey(string apiKey)
        {
            //sets the final API Key
            _client = new TelegramBotClient(apiKey);
            Logger.Log(Logger.LogLevel.Warning, "Telegram-Bot", "API Key was changed!");
        }
        
        public void Start()
        {
            //the bot will now accepts messages
            _client.StartReceiving();
            Logger.Log(Logger.LogLevel.Successful, "Telegram-Bot", "Has been started!");

            //the function is always called when he has received a message
            _client.OnMessage += Client_OnMessage;
            Logger.Log(Logger.LogLevel.Info, "Telegram-Bot", "Receives now messages!");
        }
        
        public void Stop()
        {
            //the bot no longer accepts messages
            _client.StopReceiving();
            Logger.Log(Logger.LogLevel.Successful, "Telegram-Bot", "Has been stopped!");
        }

        private void Client_OnMessage(object? sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            //check if the user is in the whitelist
            if (!EnvironmentVariable.TELEGRAM_ID_WHITELIST.Contains(Convert.ToInt32(e.Message.Chat.Id)))
            { 
                YouAreNotOnTheWhitelist(sender, e); 
                return; 
            }

            //tempary, is crappy but i know
            if (e.Message.Text.Contains("<=>"))
                SendWidget(sender, e);
            else
                SendSuggest(sender, e);
        }

        private async void SendWidget(object? sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            string rawMessage = e.Message.Text;
            
            LocationModel? locationData = WetterOnline.GetLocationData(rawMessage.Split("<=>")[1]);

            //can happend if the separator contains in the text, watch at the top "split"
            if (locationData is null)
            {
                await _client.SendTextMessageAsync(Convert.ToInt32(e.Message.Chat.Id), "An error has occurred, your input was incorrect");
                return;
            }

            string widgetHtml = WetterOnline.GetWidgetLink(locationData.geoID, locationData.locationName);

            string widgetLink = ConvertApi.HtmlToPng(widgetHtml);

            await _client.SendPhotoAsync(e.Message.Chat.Id, widgetLink);
        }

        private async void SendSuggest(object? sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            List<AutoSuggestModel>? suggests = WetterOnline.GetSuggestData(e.Message.Text);
            
            try
            {
                if (suggests.Count is 0 || suggests[0].id is null || suggests is null)
                {
                    await  _client.SendTextMessageAsync(Convert.ToInt32(e.Message.Chat.Id), "I have not found any data for this place! Please pay attention to your spelling!");
                    return;
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                await  _client.SendTextMessageAsync(Convert.ToInt32(e.Message.Chat.Id), "I have not found any data for this place! Please pay attention to your spelling!");
                return;
            }

            string message = "If your location is present, please send the respective line, i.e. \"id <=> name\"" + Environment.NewLine + Environment.NewLine;

            foreach (var suggest in suggests)
            {
                message += $"{suggest.id}<=>{suggest.n} {Environment.NewLine}";
            }

            await  _client.SendTextMessageAsync(Convert.ToInt32(e.Message.Chat.Id), message);
        }

        public static void YouAreNotOnTheWhitelist(object? sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            Logger.Log(Logger.LogLevel.Info, "Whitelist-System", $"One User wrote and was not on the Whitelist!    ID: {e.Message.Chat.Id}");
            
            _client.SendTextMessageAsync(Convert.ToInt32(e.Message.Chat.Id),
                      $"I'm sorry, I'm not allowed to serve you. " +
                      $"You are not on my whitelist. " +
                      $"Please contact my owner so he can add you! " + Environment.NewLine +
                      $"My owner is {EnvironmentVariable.TELEGRAMBOT_OWNER_NAME}" + Environment.NewLine +
                      $"(Your ID: {e.Message.Chat.Id})");
        }
    }
}
