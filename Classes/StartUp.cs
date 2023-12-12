namespace Telegram_WetterOnline_Bot.Classes
{
    public class StartUp
    {
        bool isRunning = true;
        public StartUp()
            => Start();
        private static void Start()
        {
            if (!Config.InitConfigFile())
                return;

            TelegramBot bot = new TelegramBot();

            bot.SetAPIKey(EnvironmentVariable.TELEGRAM_API_TOKEN);

            bot.Init();
            TelegramBot.StartRM();

            //ConsoleUI.StartConsole();
        }
    }
}