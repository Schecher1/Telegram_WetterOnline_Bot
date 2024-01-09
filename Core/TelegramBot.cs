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

        private async void Client_OnMessage(object? sender, MessageEventArgs e)
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
                    Logger.Log(Logger.LogLevel.Info, "Telegram-Bot", $"The Message from {e.Message.Chat.Id} is /start!");
                    
                    await _client.SendTextMessageAsync(Convert.ToInt32(e.Message.Chat.Id), "Hallo, ich bin der inoffizielle WetterOnline-Bot 🤖" + Environment.NewLine + Environment.NewLine +
                                                                                          "Ich kann dir das Wetter für die nächsten drei Tage vorhersagen 🌤" + Environment.NewLine +
                                                                                          "Dazu musst du mir nur deine Postleitzahl (oder den Namen) schicken 📬" + Environment.NewLine + Environment.NewLine +
                                                                                          "Ich werde dir dann eine Liste mit Orten schicken, die zu deiner Postleitzahl passen 📝" + Environment.NewLine +
                                                                                          "Wähle dann einfach den Ort aus, der zu dir passt 📍" + Environment.NewLine +
                                                                                          "Ich werde dir dann eine Wettervorhersage für die nächsten drei Tage schicken 📅" + Environment.NewLine + Environment.NewLine +
                                                                                          "Ich wünsche dir viel Spaß mit mir 🤗");
                    return;
                }

                //catch the time event command
                if (e.Message.Text.ToLower().Contains("/settimer"))
                {
                    if (e.Message.Text.ToLower() == "/settimer")
                    {
                        await _client.SendTextMessageAsync(e.Message.Chat.Id, "Freut mich, dass ich dich jeden Tag um einer bestimmten Uhrzeit an das Wetter erinnern darf 🤗" + Environment.NewLine +
                                                                              "Dazu musst du mir nur die Uhrzeit schicken, zu der ich dich erinnern soll ⏰" + Environment.NewLine +
                                                                              "und du musst mir schreiben, von was für ein Ort du es gerne hättest." + Environment.NewLine + Environment.NewLine +
                                                                              "Beispiel: /setTimer 12:00==Berlin" + Environment.NewLine + Environment.NewLine +
                                                                              "Ich werde dich dann jeden Tag um 12:00 Uhr an das Wetter in Berlin erinnern 🌤");
                        return;
                    }

                    if (!e.Message.Text.Contains("=="))
                    {
                        await _client.SendTextMessageAsync(e.Message.Chat.Id, "Es ist ein Fehler aufgetreten, Ihre Eingabe war falsch");
                        return;
                    }

                    string time = e.Message.Text.Replace("/setTimer", "").Trim().Split("==")[0];
                    string location = e.Message.Text.Replace("/setTimer", "").Trim().Split("==")[1];
                    LocationModel? locationModel = WetterOnline.GetLocationData(location);
                    string locationName = locationModel?.locationName ?? "";

                    var inlineKeyboard = new InlineKeyboardMarkup(new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Ja, passt!", $"setTimerYes_{time}=={location}"),
                            InlineKeyboardButton.WithCallbackData("Oh nein, passt nicht", "setTimerNo")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Schick mir bitte ein Beispiel Bild", $"setTimeSendTestImage_{locationName}")
                        }
                    });

                    await _client.SendTextMessageAsync(e.Message.Chat.Id, "Habe ich das richtig verstanden?" + Environment.NewLine +
                                                                          $"Du möchtest jeden Tag um {time} Uhr an das Wetter in {location} erinnert werden?", replyMarkup: inlineKeyboard);
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

            if (callbackQuery.Data.StartsWith("setTimerYes_"))
            {
                int hour = Convert.ToInt32(callbackQuery.Data.Replace("setTimerYes_", "").Split("==")[0].Split(":")[0]);
                int minute = Convert.ToInt32(callbackQuery.Data.Replace("setTimerYes_", "").Split("==")[0].Split(":")[1]);

                TimerEventModel newEvent = new TimerEventModel()
                {
                    ChatId = chatId,
                    Time = new TimeSpan(hour, minute, 0),
                    Location = callbackQuery.Data.Replace("setTimerYes_", "").Split("==")[1]
                };
                await _client.SendTextMessageAsync(chatId, $"Alles klar, ich werde dich um {callbackQuery.Data.Replace("setTimerYes_", "").Split("==")[0]} Uhr an das Wetter von {callbackQuery.Data.Replace("setTimerYes_", "").Split("==")[1]} erinnern 🌤");
                await _client.SendTextMessageAsync(chatId, JsonConvert.SerializeObject(newEvent, Formatting.Indented));
                await _client.DeleteMessageAsync(chatId, callbackQuery.Message.MessageId);
            }

            if (callbackQuery.Data.StartsWith("setTimerNo"))
            {
                await _client.SendTextMessageAsync(chatId, "Dann versuche es doch nochmal, ich warte auf deine Eingabe 😊");
                await _client.DeleteMessageAsync(chatId, callbackQuery.Message.MessageId);
            }

            if (callbackQuery.Data.StartsWith("setTimeSendTestImage_"))
            {
                // Extract the suggest ID from the callback data
                var nameOfLocation = callbackQuery.Data.Substring("setTimeSendTestImage_".Length);
                LocationModel locationData = WetterOnline.GetLocationData(nameOfLocation);
                await SendWidget(locationData, chatId);
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

        public static async void YouAreNotOnTheWhitelist(object? sender, MessageEventArgs e)
        {
            Logger.Log(Logger.LogLevel.Info, "Whitelist-System", $"One User wrote and was not on the Whitelist!    ID: {e.Message.Chat.Id}");

            await _client.SendTextMessageAsync(Convert.ToInt32(e.Message.Chat.Id), "Hallo, ich bin der inoffizielle WetterOnline-Bot 🤖" + Environment.NewLine + Environment.NewLine +
                                                          "Ich kann dir das Wetter für die nächsten drei Tage vorhersagen 🌤" + Environment.NewLine +
                                                          "Dazu musst du mir nur deine Postleitzahl (oder den Namen) schicken 📬" + Environment.NewLine + Environment.NewLine +
                                                          "Ich werde dir dann eine Liste mit Orten schicken, die zu deiner Postleitzahl passen 📝" + Environment.NewLine +
                                                          "Wähle dann einfach den Ort aus, der zu dir passt 📍" + Environment.NewLine +
                                                          "Ich werde dir dann eine Wettervorhersage für die nächsten drei Tage schicken 📅" + Environment.NewLine + Environment.NewLine + Environment.NewLine +
                                                          "Aber leider haben wir ein Problem, ich darf Ihnen keine Wettervorhersage schicken, da Sie nicht auf der Whitelist stehen! 😔" + Environment.NewLine +
                                                         $"Bitte kontaktieren Sie meinen Besitzer, damit er Sie hinzufügen kann! " + Environment.NewLine +
                                                         $"Mein Besitzer ist {EnvironmentVariable.TELEGRAMBOT_OWNER_NAME}" + Environment.NewLine + Environment.NewLine +
                                                        $"(Ihre ID: {e.Message.Chat.Id})" + Environment.NewLine + Environment.NewLine +
                                                        "Pscchhht... Wenn der Besitzer Sie nicht hinzufügen möchte, dann können Sie auch den Code auf GitHub herunterladen und mich selbst hosten! 🤫 (https://github.com/Schecher1/Telegram_WetterOnline_Bot)");
        }
    }
}
