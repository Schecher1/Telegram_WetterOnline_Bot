namespace Telegram_WetterOnline_Bot.Model
{
    public class LogModel
    {
        public Logger.LogLevel LogLevel { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Prefix { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
