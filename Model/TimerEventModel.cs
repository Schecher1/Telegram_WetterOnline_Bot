namespace Telegram_WetterOnline_Bot.Model
{
    public class TimerEventModel
    {
        public long ChatId { get; set; }
        public string Location { get; set; }
        public TimeSpan Time { get; set; }
    }
}
