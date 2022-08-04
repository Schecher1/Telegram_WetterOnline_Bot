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

                    case "confs":
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

                    case "q":
                        isRunning = false;
                        break;
                }
            }
        }

        private static void WriteStartScrren()
        {
            cls();
            Console.WriteLine($"srm+      =>   Start Revice Messages");
            Console.WriteLine($"srm-      =>   Stop Revice Messages");
            Console.WriteLine($"confs     =>   Show the current Configs");
            Console.WriteLine($"rl        =>   Reload Config File");
            Console.WriteLine($"stat      =>   Show the status");
            Console.WriteLine($"logs      =>   Show the Log-Terminal");
            Console.WriteLine($"errlogs   =>   Shows all Error logs");
            Console.WriteLine($"warlogs   =>   Shows all Warning logs");
            Console.WriteLine($"scslogs   =>   Shows all Successful logs");
            Console.WriteLine($"q         =>   Stop the Bot");
        }

        private static void cls()
            => Console.Clear();


        private static void StartReviceMessages()
        {

        }

        private static void StopReviceMessages()
        {

        }

        private static void ShowTheCurrentConfigs()
        {

        }

        private static void ReloadConfigFile()
        {

        }

        private static void ShowTheStatus()
        {

        }

        private static void ShowTheLogTerminal()
        {

        }

        private static void ShowsAllErrorLogs()
        {

        }

        private static void ShowsAllWarningLogs()
        {

        }

        private static void ShowsAllSuccessfulLogs()
        {

        }
    }
}
