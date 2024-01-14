using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public static List<TimerEventModel> GetJobs()
        {
            CheckWorkDir();
            LoadAllEvents();

            return _events.Where(x => x.IsDue()).ToList();
        }
    }
}
