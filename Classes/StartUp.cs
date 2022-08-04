namespace Telegram_WetterOnline_Bot.Classes
{
    public class StartUp
    {
        bool isRunning = true;
        public StartUp()
            => Start();
        private void Start()
        {
            if (!Config.InitConfigFile())
                return;

            TelegramBot bot = new TelegramBot();

            bot.SetAPIKey(EnvironmentVariable.TELEGRAM_API_TOKEN);

            bot.Start();

            //console ui, for log, start, edit file, etc.
            //comes in a separate version
            while (isRunning)
            {
                if (Console.ReadLine() == "q")
                    isRunning = false;

            }
        }
    }
}