namespace Telegram_WetterOnline_Bot.Handler
{
    public class TimeEventHandler
    {
        const int DELAY = 1000; //in milliseconds
        public TimeEventHandler(TelegramBot __telegramBotClass)
        {
            Logger.Log(Logger.LogLevel.Info, "TimeEventHandler", "Event Task started");
            Task.Run(() => EventTask(__telegramBotClass));
        }

        private  void EventTask(TelegramBot __telegramBotClass)
        {
            for (; ; )
            {
                //wait 1 second before checking again
                Task.Delay(DELAY).Wait();

                //get all tasks that are due
                List<TimerEventModel> tasks = DataHandler.GetDueJobs();

                //continue if there are no tasks
                if (tasks.Count is 0) 
                    continue;

                //send the widget to all chats that are due
                foreach (TimerEventModel task in tasks)
                {
                    __telegramBotClass.SendWidget(WetterOnline.GetLocationData(task.Location), task.ChatId);
                    task.LastSend = DateTime.Now;
                    DataHandler.UpdateLastSend(task.Id);
                }
            }
        }
    }
}
