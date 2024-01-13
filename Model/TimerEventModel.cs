namespace Telegram_WetterOnline_Bot.Model
{
    public class TimerEventModel
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public long ChatId { get; set; }
        public string Location { get; set; }
        public TimeSpan Time { get; set; }
        public DateTime LastSend { get; set; } = default;

        const int MAX_TIME_AFTER = 2; //in minutes

        public bool IsDue()
        {
            return DateTime.Now.TimeOfDay >= Time && 
                       DateTime.Now.TimeOfDay <= Time + TimeSpan.FromMinutes(MAX_TIME_AFTER);
        }
    }
}
