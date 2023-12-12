namespace Telegram_WetterOnline_Bot.Classes
{
    public class StartUp
    {
        bool isRunning = true;
        public StartUp()
            => Start();
        private static void Start()
        {
            //get env vars 
            string telegramToken = Environment.GetEnvironmentVariable("TELEGRAM_API_TOKEN");
            string telegramOwnerName = Environment.GetEnvironmentVariable("TELEGRAMBOT_OWNER_NAME");
            int[] telegramIDWhitelist = Array.ConvertAll(Environment.GetEnvironmentVariable("TELEGRAM_ID_WHITELIST").Split(';'), int.Parse);

            //check if env vars are empty
            if (string.IsNullOrEmpty(telegramToken) || string.IsNullOrEmpty(telegramOwnerName) || telegramIDWhitelist is null)
            {
                Console.WriteLine("One or more Environment Variables are empty!");
                Console.WriteLine("Telegram_API_Token: " + telegramToken);
                Console.WriteLine("Telegrambot_Owner_Name: " + telegramOwnerName);
                Console.WriteLine("Telegrambot_ID_Whitelist: " + telegramIDWhitelist);
                return;
            }

            TelegramBot bot = new TelegramBot();

            bot.SetAPIKey(EnvironmentVariable.TELEGRAM_API_TOKEN);

            bot.Init();
            TelegramBot.StartRM();
        }
    }
}