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

    }
}
