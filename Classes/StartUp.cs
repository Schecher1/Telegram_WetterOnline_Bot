namespace Telegram_WetterOnline_Bot.Classes
{
    public class StartUp
    {
        public StartUp()
            => Start();

        private void Start()
        {
            //get env vars 
            try
            {
                string telegramToken = Environment.GetEnvironmentVariable("TELEGRAM_API_TOKEN");
                string telegramOwnerName = Environment.GetEnvironmentVariable("TELEGRAMBOT_OWNER_NAME");
                int[] telegramIDWhitelist = Array.ConvertAll(Environment.GetEnvironmentVariable("TELEGRAM_ID_WHITELIST").Split(';'), int.Parse);
                bool isDebug = Environment.GetEnvironmentVariable("DEBUG") != null;

                //check if env vars are empty
                if (string.IsNullOrEmpty(telegramToken) || string.IsNullOrEmpty(telegramOwnerName) || telegramIDWhitelist is null)
                {
                    Console.WriteLine("One or more Environment Variables are empty!");
                    Console.WriteLine("Telegram_API_Token: " + telegramToken);
                    Console.WriteLine("Telegrambot_Owner_Name: " + telegramOwnerName);
                    Console.WriteLine("Telegrambot_ID_Whitelist: " + telegramIDWhitelist);
                    return;
                }

                //set env data
                EnvironmentVariable.TELEGRAM_API_TOKEN = telegramToken;
                EnvironmentVariable.TELEGRAMBOT_OWNER_NAME = telegramOwnerName;
                EnvironmentVariable.TELEGRAM_ID_WHITELIST = telegramIDWhitelist;
                EnvironmentVariable.IS_DEBUG = isDebug;

                //bot init
                TelegramBot bot = new TelegramBot();
                bot.SetAPIKey(EnvironmentVariable.TELEGRAM_API_TOKEN);
                bot.Init();
                bot.StartRM();

                //let the docker container run 4 ever
                Thread.Sleep(Timeout.Infinite);
            }
            catch (Exception ex)
            {
                Console.WriteLine("One or more Environment Variables are empty!");
                Console.WriteLine("Telegram_API_Token: " + Environment.GetEnvironmentVariable("TELEGRAM_API_TOKEN"));
                Console.WriteLine("Telegrambot_Owner_Name: " + Environment.GetEnvironmentVariable("TELEGRAMBOT_OWNER_NAME"));
                Console.WriteLine("Telegrambot_ID_Whitelist: " + Environment.GetEnvironmentVariable("TELEGRAM_ID_WHITELIST"));
                Console.WriteLine(ex.Message);
            }
        }
    }
}