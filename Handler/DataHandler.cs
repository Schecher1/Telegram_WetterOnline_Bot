namespace Telegram_WetterOnline_Bot.Handler
{
    public class DataHandler
    {
        private const string JSON_PATH = @"./data/";
        private const string JSON_FILE = "metaData.json";
        private static List<TimerEventModel> _events = new List<TimerEventModel>();

        
        private static void CheckWorkDir()
        {
            if (!Directory.Exists(JSON_PATH))
                Directory.CreateDirectory(JSON_PATH);
        }
        
        private static void LoadAllEvents()
        {
            CheckWorkDir();

            if (!File.Exists(JSON_PATH + JSON_FILE))
                File.WriteAllText(JSON_PATH + JSON_FILE, "[]");
            
            _events.Clear();
            _events = JsonConvert.DeserializeObject<List<TimerEventModel>>(File.ReadAllText(JSON_PATH + JSON_FILE));
        }

        private static void SaveChanges()
        {
            CheckWorkDir();

            if (!File.Exists(JSON_PATH + JSON_FILE))
                File.WriteAllText(JSON_PATH + JSON_FILE, "[]");

            File.WriteAllText(JSON_PATH + JSON_FILE, JsonConvert.SerializeObject(_events, Formatting.Indented));
        }

        
        
        public static void AddTimeEvent(TimerEventModel newEvent)
        {
            CheckWorkDir();
            LoadAllEvents();
            
            _events.Add(newEvent);
            SaveChanges();
        }

        public static void RemoveTimeEvent(Guid id)
        {
            CheckWorkDir();
            LoadAllEvents();
            
            _events.RemoveAll(x => x.Id == id);
            SaveChanges();
        }

        //means that he returns a list of events that are due (max 2 minute in past)
        public static List<TimerEventModel> GetDueJobs()
        {
            CheckWorkDir();
            LoadAllEvents();

            return _events.Where(x => x.IsDue()).ToList();
        }

        public static List<TimerEventModel> GetAllJobs(long chatId)
        {
            CheckWorkDir();
            LoadAllEvents();

            return _events.Where(x => x.ChatId == chatId).ToList();
        }

        internal static void UpdateLastSend(Guid id)
        {
            CheckWorkDir();
            LoadAllEvents();

            _events.Find(x => x.Id == id).LastSend = DateTime.Now;
            SaveChanges();
        }
        
        public static void RemoveUser(long id)
        {
            CheckWorkDir();
            LoadAllEvents();

            _events.RemoveAll(x => x.ChatId == id);
            SaveChanges();
        }

        internal static bool CheckOwner(long chatId, Guid jobId)
        {
            CheckWorkDir();
            LoadAllEvents();

            try
            {
                return _events.Find(x => x.Id == jobId).ChatId == chatId;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
