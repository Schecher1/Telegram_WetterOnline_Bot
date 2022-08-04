namespace Telegram_WetterOnline_Bot.Core
{
    public class Logger
    {
        public static void Log(LogLevel logLevel, string logPrefix, string logMessage)
        {
            switch (logLevel)
            {
                case LogLevel.Debug:
                    if (EnvironmentVariable.SYSTEM_LogTerminalIsOpen)
                        WriteLog(ConsoleColor.DarkGray, "Debug", logPrefix, logMessage);
                    SaveLog(logLevel, logPrefix, logMessage);
                    WriteLogIntoFile("Debug", logPrefix, logMessage);
                    break;
                case LogLevel.Info:
                    if (EnvironmentVariable.SYSTEM_LogTerminalIsOpen)
                        WriteLog(ConsoleColor.White, "Info", logPrefix, logMessage);
                    SaveLog(logLevel, logPrefix, logMessage);
                    WriteLogIntoFile("Info", logPrefix, logMessage);
                    break;
                case LogLevel.Successful:
                    if (EnvironmentVariable.SYSTEM_LogTerminalIsOpen)
                        WriteLog(ConsoleColor.Green, "Successful", logPrefix, logMessage);
                    SaveLog(logLevel, logPrefix, logMessage);
                    WriteLogIntoFile("Successful", logPrefix, logMessage);
                    break;
                case LogLevel.Warning:
                    if (EnvironmentVariable.SYSTEM_LogTerminalIsOpen)
                        WriteLog(ConsoleColor.DarkYellow, "Warning", logPrefix, logMessage);
                    SaveLog(logLevel, logPrefix, logMessage);
                    WriteLogIntoFile("Warning", logPrefix, logMessage);
                    break;
                case LogLevel.Error:
                    if (EnvironmentVariable.SYSTEM_LogTerminalIsOpen)
                        WriteLog(ConsoleColor.Red, "Error", logPrefix, logMessage);
                    SaveLog(logLevel, logPrefix, logMessage);
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

        private static void SaveLog(LogLevel logLevel, string logPrefix, string logMessage)
        {
            LogModel log = new LogModel()
            {
                LogLevel = logLevel,
                Prefix = logPrefix,
                Message = logMessage,
                Date = DateTime.Now
            };

            EnvironmentVariable.SYSTEM_LOG.Add(log);
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
            Error,
            All
        }
    }
}