namespace Telegram_WetterOnline_Bot.Core
{
    public class Config
    {
        public static bool allWasRight = true;
        
        public static bool InitConfigFile()
        {
            try
            {
                Logger.Log(Logger.LogLevel.Info, "Config-Manager", "An attempt is now being made to read out the Config file.");

                if (!Directory.Exists(@"data"))
                    Directory.CreateDirectory(@"data");

                if (!File.Exists(@"data/config.json"))
                {
                    CreateNewConfigFile();
                    allWasRight = false;
                }

                ConfigModel? config = JsonConvert.DeserializeObject<ConfigModel>(File.ReadAllText(@"data/config.json"));

                if (config is null)
                {
                    Logger.Log(Logger.LogLevel.Error, "Config-Manager", "The Config file is empty or corrupt. A new one will be created.");
                    CreateNewConfigFile();
                    allWasRight = false;
                    config = JsonConvert.DeserializeObject<ConfigModel>(File.ReadAllText(@"data/config.json"));
                }

                SetConfig(config);
                if (allWasRight)
                    Logger.Log(Logger.LogLevel.Info, "Config-Manager", "The Config file has been read out successfully.");
                return allWasRight;
            }
            catch
            {
                Logger.Log(Logger.LogLevel.Error, "Config-Manager", "An error occured while trying to read out the Config file.");
                allWasRight = false;
                return allWasRight;
            }
            
    }

        private static void CreateNewConfigFile()
        {
            Logger.Log(Logger.LogLevel.Error, "Config-Manager", "No conifg file was found, it will be created now.");
         
            if (File.Exists(@"data/config.json"))
                File.Delete(@"data/config.json");

            ConfigModel config = new ConfigModel()
            {
                Telegram_API_Token = "0000000000:AAAAA-aaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                Convert_API_Token = "aaaaaaaaaaaaaaaa",
                Telegrambot_Owner_Name = "@YouTelegramUsername",
                Telegrambot_ID_Whitelist = new int[] { 000000000 }
            };

            File.WriteAllText(@"data/config.json", JsonConvert.SerializeObject(config, Formatting.Indented));

            Logger.Log(Logger.LogLevel.Successful, "Config-Manager", "The config file has been generated. Please complete the file and start the bot again (Path: data/config.json)");
        }

        private static void SetConfig(ConfigModel? config)
        {
            EnvironmentVariable.TELEGRAM_API_TOKEN = config.Telegram_API_Token;
            EnvironmentVariable.CONVERT_API_TOKEN = config.Convert_API_Token;
            EnvironmentVariable.TELEGRAMBOT_OWNER_NAME = config.Telegrambot_Owner_Name;
            EnvironmentVariable.TELEGRAM_ID_WHITELIST = config.Telegrambot_ID_Whitelist;
        }
    }
}
