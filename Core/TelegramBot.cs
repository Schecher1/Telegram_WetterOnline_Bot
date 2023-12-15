using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

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

        public void Init()
        {
            //the function is always called when he has received a message
            _client.OnMessage += Client_OnMessage;

            //the function is always called when he has received a callback
            _client.OnCallbackQuery += OnCallbackQueryReceived;
        }

        public void StartRM()
        {
            //the bot will now accepts messages
            _client.StartReceiving();
            Logger.Log(Logger.LogLevel.Successful, "Telegram-Bot", "Has been started!");
            Logger.Log(Logger.LogLevel.Info, "Telegram-Bot", "Receives now messages!");
        }

        public void StopRM()
        {
            //the bot no longer accepts messages
            try
            {
                _client.StopReceiving();
                Logger.Log(Logger.LogLevel.Successful, "Telegram-Bot", "Has been stopped!");
                Logger.Log(Logger.LogLevel.Warning, "Telegram-Bot", "Do not receive any more messages!");
            }
            catch (Exception ex)
            {
                Logger.Log(Logger.LogLevel.Error, "Telegram-Bot", ex.Message);
            }
        }

        private async void Client_OnMessage(object? sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            try
            {
                //check if the user is in the whitelist
                if (!EnvironmentVariable.TELEGRAM_ID_WHITELIST.Contains(Convert.ToInt32(e.Message.Chat.Id)))
                {
                    YouAreNotOnTheWhitelist(sender, e);
                    return;
                }

                if (e.Message.Text is null)
                {
                    Logger.Log(Logger.LogLevel.Info, "Telegram-Bot", $"The Message from {e.Message.Chat.Id} is null!");
                    await _client.SendTextMessageAsync(Convert.ToInt32(e.Message.Chat.Id), "Es ist ein Fehler aufgetreten, Ihre Eingabe war falsch");
                    return;
                }

                //catch the start command
                if (e.Message.Text is "/start")
                {
                    await _client.SendTextMessageAsync(Convert.ToInt32(e.Message.Chat.Id), "Hallo, ich bin der inoffizielle WetterOnline-Bot 🤖" + Environment.NewLine + Environment.NewLine +
                                                                                          "Ich kann dir das Wetter für die nächsten drei Tage vorhersagen 🌤" + Environment.NewLine +
                                                                                          "Dazu musst du mir nur deine Postleitzahl (oder den Namen) schicken 📬" + Environment.NewLine + Environment.NewLine +
                                                                                          "Ich werde dir dann eine Liste mit Orten schicken, die zu deiner Postleitzahl passen 📝" + Environment.NewLine +
                                                                                          "Wähle dann einfach den Ort aus, der zu dir passt 📍" + Environment.NewLine +
                                                                                          "Ich werde dir dann eine Wettervorhersage für die nächsten drei Tage schicken 📅" + Environment.NewLine + Environment.NewLine +
                                                                                          "Ich wünsche dir viel Spaß mit mir 🤗");
                    return;
                }

                SendSuggest(sender, e);
            }
            catch (Exception ex)
            {
                Logger.Log(Logger.LogLevel.Error, "Telegram-Bot", ex.Message);
            }
        }

        private async void OnCallbackQueryReceived(object sender, CallbackQueryEventArgs e)
        {
            // e.CallbackQuery contains information about the triggered action
            var callbackQuery = e.CallbackQuery;
            var chatId = callbackQuery.Message.Chat.Id;

            // Process the callback data that you specified when creating the inline keyboard
            // In this example, we assume that the callback data is "button_{suggest.id}"
            if (callbackQuery.Data.StartsWith("choice_"))
            {
                // Extract the suggest ID from the callback data
                var nameOfLocation = callbackQuery.Data.Substring("choice_".Length);
                LocationModel locationData = WetterOnline.GetLocationData(nameOfLocation);
                await SendWidget(locationData, chatId);

                //delete the message (to keep the chat clean)
                await _client.DeleteMessageAsync(chatId, callbackQuery.Message.MessageId);
            }
        }

        private async Task SendWidget(LocationModel locationData, long ChatId)
        {
            try
            {
                string widgetHtml = WetterOnline.GetWidgetLink(locationData.geoID, locationData.locationName);
                string pathToWidget = await Converter.HtmlToJpeg(widgetHtml);

                if (pathToWidget == String.Empty || pathToWidget is null)
                {
                    await _client.SendTextMessageAsync(Convert.ToInt32(ChatId), "Es ist ein Fehler aufgetreten, bitte rufen Sie einen Admin an!");
                    return;
                }

                using (var stream = new FileStream(pathToWidget, FileMode.Open))
                {
                    string textMessage = $"Das sind die Wettervorhersagen für die nächsten drei Tage für {locationData.zipCode} {locationData.locationName} ({locationData.subStateID}) 🌤" + Environment.NewLine +
                                         $"Heute ist der {DateTime.Today.ToString("dd.MM.yyyy")} 📅 um {DateTime.Now.ToString("HH:mm")} Uhr 🕔" + Environment.NewLine + Environment.NewLine +
                                         $"Für weitere Informationen besuchen Sie: {locationData.url}" + Environment.NewLine + Environment.NewLine +
                                         $"© Entwickler @Schecher_1" + Environment.NewLine;

                    await _client.SendPhotoAsync(ChatId, stream, textMessage);
                    Logger.Log(Logger.LogLevel.Successful, "TelegramBot", $"Send Widget to {ChatId}!");
                }
            }
            catch (Exception ex)
            {
                Logger.Log(Logger.LogLevel.Error, "TelegramBot", $"Send Widget was requestet!   ERROR: {ex.Message}");
            }
        }

        private async void SendSuggest(object? sender, MessageEventArgs e)
        {
            List<AutoSuggestModel>? suggests = WetterOnline.GetSuggestData(e.Message.Text);


            //if there is just one suggest, then accept it as the right one
            if (suggests.Count is 1)
            {
                //manipulate the message, for the method "SendWidget"
                e.Message.Text = suggests[0].id + "<=>" + suggests[0].n;

                LocationModel? locationData = WetterOnline.GetLocationData(suggests[0].n);

                if (locationData is null)
                {
                    await _client.SendTextMessageAsync(Convert.ToInt32(e.Message.Chat.Id), "Ich habe keine Daten zu diesem Ort gefunden! Bitte achten Sie auf Ihre Rechtschreibung!");
                    return;
                }

                SendWidget(locationData, e.Message.Chat.Id);
                return;
            }

            //check if there are any suggests
            if (suggests.Count is 0 || suggests[0].id is null || suggests is null)
            {
                await _client.SendTextMessageAsync(Convert.ToInt32(e.Message.Chat.Id), "Ich habe keine Daten zu diesem Ort gefunden! Bitte achten Sie auf Ihre Rechtschreibung!");
                return;
            }

            List<List<InlineKeyboardButton>> keyboardButtons = new List<List<InlineKeyboardButton>>();
            foreach (var suggest in suggests)
            {
                var button = new InlineKeyboardButton
                {
                    Text = suggest.n,
                    CallbackData = $"choice_{suggest.n}"
                };

                keyboardButtons.Add(new List<InlineKeyboardButton> { button });
            }

            var inlineKeyboard = new InlineKeyboardMarkup(keyboardButtons);

            await _client.SendTextMessageAsync(
                Convert.ToInt32(e.Message.Chat.Id),
                "Hier ist eine Liste von möglichen Orten, die Sie meinen könnten:",
                replyMarkup: inlineKeyboard
            );
        }

        public static void YouAreNotOnTheWhitelist(object? sender, MessageEventArgs e)
        {
            Logger.Log(Logger.LogLevel.Info, "Whitelist-System", $"One User wrote and was not on the Whitelist!    ID: {e.Message.Chat.Id}");

            _client.SendTextMessageAsync(Convert.ToInt32(e.Message.Chat.Id),
                      $"Es tut mir leid, ich darf Sie nicht bedienen. " + Environment.NewLine +
                      $"Sie sind nicht auf meiner Whitelist. " +
                      $"Bitte kontaktieren Sie meinen Besitzer, damit er Sie hinzufügen kann! " + Environment.NewLine +
                      $"Mein Besitzer ist {EnvironmentVariable.TELEGRAMBOT_OWNER_NAME}" + Environment.NewLine +
                      $"(Ihre ID: {e.Message.Chat.Id})");
        }
    }
}
