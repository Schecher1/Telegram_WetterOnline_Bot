namespace Telegram_WetterOnline_Bot.Core
{
    public class ConsoleUI
    {
        static bool isRunning = true;

        public static void StartConsole()
        {
            while (isRunning)
            {
                WriteStartScrren();

                string? cmd = Console.ReadLine().ToLower();

                switch (cmd)
                {
                    case "srm+":
                        StartReviceMessages();
                        break;

                    case "srm-":
                        StopReviceMessages();
                        break;

                    case "conf":
                        ShowTheCurrentConfigs();
                        break;

                    case "rl":
                        ReloadConfigFile();
                        break;

                    case "stat":
                        ShowTheStatus();
                        break;

                    case "logs":
                        ShowTheLogTerminal();
                        break;

                    case "errlogs":
                        ShowsAllErrorLogs();
                        break;

                    case "warlogs":
                        ShowsAllWarningLogs();
                        break;

                    case "scslogs":
                        ShowsAllSuccessfulLogs();
                        break;

                    case "inflogs":
                        ShowsAllInfoLogs();
                        break;

                    case "dbglogs":
                        ShowsAllDebugLogs();
                        break;

                    case "q":
                        isRunning = false;
                        break;
                }
            }
        }

        private static void WriteStartScrren()
        {
            cls();
            Console.WriteLine("     Welcome to the Console UI for Telegram Bot!");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine($"srm+        =>   Start Revice Messages");
            Console.WriteLine($"srm-        =>   Stop Revice Messages");
            Console.WriteLine($"conf        =>   Show the current Configs");
            Console.WriteLine($"rl          =>   Reload Config File");
            Console.WriteLine($"stat        =>   Show the status");
            Console.WriteLine($"logs        =>   Show the Log-Terminal");
            Console.WriteLine($"errlogs     =>   Shows all Error logs");
            Console.WriteLine($"warlogs     =>   Shows all Warning logs");
            Console.WriteLine($"scslogs     =>   Shows all Successful logs");
            Console.WriteLine($"inflogs     =>   Shows all Info logs");
            Console.WriteLine($"dbglogs     =>   Shows all Debug logs");
            Console.WriteLine($"q           =>   Stop the Bot");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.Write(">");
        }

        private static void cls()
            => Console.Clear();

        private static void StartReviceMessages()
            => TelegramBot.StartRM();

        private static void StopReviceMessages()
            => TelegramBot.StopRM();

        private static void ShowTheCurrentConfigs()
        {
            cls();
            Console.WriteLine("     Configs for the Telegram Bot!");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine($"Telegram_API_Token: {EnvironmentVariable.TELEGRAM_API_TOKEN}" + Environment.NewLine);
            Console.WriteLine($"Convert_API_Token: {EnvironmentVariable.CONVERT_API_TOKEN}" + Environment.NewLine);
            Console.WriteLine($"Telegrambot_Owner_Name: {EnvironmentVariable.TELEGRAMBOT_OWNER_NAME}" + Environment.NewLine);
            Console.WriteLine($"Telegrambot_ID_Whitelist: {string.Join(", ", EnvironmentVariable.TELEGRAM_ID_WHITELIST)}");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.ReadKey();
        }

        private static void ReloadConfigFile()
        {
            cls();
            TelegramBot.StopRM();
            Config.InitConfigFile();
            TelegramBot.StartRM();
            Logger.Log(Logger.LogLevel.Successful, "System", "Config File reloaded!");
        }
        
        private static void ShowTheStatus()
        {
            cls();
            Console.WriteLine("     Status from the Telegram Bot!");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine($"srm telegrambot =>  {(TelegramBot._client.IsReceiving ? "Running" : "Stopped")}");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.ReadKey();
        }

        private static void ShowTheLogTerminal()
            => WriteAllLogsFromList(Logger.LogLevel.All);

        private static void ShowsAllErrorLogs()
            => WriteAllLogsFromList(Logger.LogLevel.Error);

        private static void ShowsAllWarningLogs()
            => WriteAllLogsFromList(Logger.LogLevel.Warning);

        private static void ShowsAllSuccessfulLogs()
            => WriteAllLogsFromList(Logger.LogLevel.Successful);

        private static void ShowsAllInfoLogs()
            => WriteAllLogsFromList(Logger.LogLevel.Info);

        private static void ShowsAllDebugLogs()
            => WriteAllLogsFromList(Logger.LogLevel.Debug);

        private static void WriteAllLogsFromList(Logger.LogLevel SortLogLevel)
        {
            cls();
            
            if (EnvironmentVariable.SYSTEM_LOG.Count != 0)
                foreach (LogModel log in EnvironmentVariable.SYSTEM_LOG)
                {

                    //sort the logs
                    //check if all logs should be shown
                    if (Logger.LogLevel.All != SortLogLevel)
                        //check if the log level is the same as the sort log level
                        if (log.LogLevel != SortLogLevel)
                            continue;


                    //reset the font color to default
                    Console.ResetColor();

                    //write date and time
                    Console.Write($"{log.Date}: ");

                    //write log prefix
                    switch (log.LogLevel)
                    {
                        case Logger.LogLevel.Debug:
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            break;
                        case Logger.LogLevel.Info:
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        case Logger.LogLevel.Successful:
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                        case Logger.LogLevel.Warning:
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            break;
                        case Logger.LogLevel.Error:
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                    }
                    Console.Write($"{log.LogLevel.ToString()} ");

                    //write log message prefix
                    Console.Write($"[{log.Prefix}] ");

                    //write log message
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"=> {log.Message}");

                    //end the log
                    Console.Write(Environment.NewLine);

                    //reset the font color to default
                    Console.ResetColor();
                }

            //So that the new logs are also displayed
            EnvironmentVariable.SYSTEM_LogTerminalIsOpen = true;

            Console.ReadKey();

            //So that the new logs are no longer displayed
            EnvironmentVariable.SYSTEM_LogTerminalIsOpen = false;
        }
    }
}
