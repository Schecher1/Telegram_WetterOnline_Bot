namespace Telegram_WetterOnline_Bot.Core
{
    public class Logger
    {
        public static void Log(LogLevel logLevel, string logPrefix, string logMessage)
        {
            switch (logLevel)
            {
                case LogLevel.Debug:
                    WriteLog(ConsoleColor.DarkGray, "Debug", logPrefix, logMessage);
                    WriteLogIntoFile("Debug", logPrefix, logMessage);
                    break;
                case LogLevel.Info:
                    WriteLog(ConsoleColor.White, "Info", logPrefix, logMessage);
                    WriteLogIntoFile("Info", logPrefix, logMessage);
                    break;
                case LogLevel.Successful:
                    WriteLog(ConsoleColor.Green, "Successful", logPrefix, logMessage);
                    WriteLogIntoFile("Successful", logPrefix, logMessage);
                    break;
                case LogLevel.Warning:
                    WriteLog(ConsoleColor.DarkYellow, "Warning", logPrefix, logMessage);
                    WriteLogIntoFile("Warning", logPrefix, logMessage);
                    break;
                case LogLevel.Error:
                    WriteLog(ConsoleColor.Red, "Error", logPrefix, logMessage);
                    WriteLogIntoFile("Error", logPrefix, logMessage);
                    break;
            }
        }
        
        private static void WriteLog(ConsoleColor prefixColor, string logLevelPrefix, string logPrefix, string logMessage)
        {
            //reset the font color to default
            Console.ResetColor();
            
            //write date and time
            Console.Write($"{DateTime.Now}: ");

            //write log prefix
            Console.ForegroundColor = prefixColor;
            Console.Write($"{logLevelPrefix} ");

            //write log message prefix
            Console.Write($"[{logPrefix}] ");

            //write log message
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"=> {logMessage}");

            //end the log
            Console.Write(Environment.NewLine);

            //reset the font color to default
            Console.ResetColor();
        }

        private async static void WriteLogIntoFile(string logLevelPrefix , string logPrefix, string logMessage)
        {
            if (!Directory.Exists(@"data"))
                Directory.CreateDirectory(@"data");
            
            if (!Directory.Exists(@"data/log"))
                Directory.CreateDirectory(@"data/log");

            //Add the log to the file
            try
            {
                await File.AppendAllTextAsync
                (
                    @$"data/log/log_from_{DateTime.Now.ToString("dd_MM_yyyy")}.txt",
                    $"{DateTime.Now}: {logLevelPrefix} [{logPrefix}] => {logMessage} {Environment.NewLine}"
                );
            }
            catch (Exception ex)
            {
                Logger.Log(logLevel: LogLevel.Error, "Logger", ex.Message);
            }
        }
        
        public enum LogLevel
        {
            Debug,
            Info,
            Successful,
            Warning,
            Error
        }
    }
}