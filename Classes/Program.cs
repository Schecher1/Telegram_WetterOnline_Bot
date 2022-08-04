namespace Telegram_WetterOnline_Bot.Classes
{
    internal class Program
    {
        static void Main()
        {
            Logger.Log(Logger.LogLevel.Info, "Program", $"The program version: {EnvironmentVariable.SYSTEM_VERSION}");
            Logger.Log(Logger.LogLevel.Info, "Program", "The program has been started!");
            new StartUp();
            Logger.Log(Logger.LogLevel.Info, "Program", "The program has been exited!");
        }
    }
}