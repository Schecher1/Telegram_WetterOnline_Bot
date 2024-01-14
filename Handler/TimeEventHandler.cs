using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Telegram_WetterOnline_Bot.Handler
{
    public class TimeEventHandler
    {
        public TimeEventHandler(TelegramBot __telegramBotClass)
        {
            Task.Run(() => EventTask(__telegramBotClass));
        }

        private  void EventTask(TelegramBot __telegramBotClass)
        {
            for (; ; )
            {
                List<TimerEventModel> tasks = DataHandler.GetJobs();

                if (tasks.Count is 0) 
                    continue;

                foreach (TimerEventModel task in tasks)
                {
                    Console.WriteLine("try send");
                    __telegramBotClass.SendWidget(WetterOnline.GetLocationData(task.Location), task.ChatId);     
                    task.LastSend = DateTime.Now;
                }
                
                Task.Delay(1000).Wait();
                Console.WriteLine("tick");
            }
        }
    }
}
