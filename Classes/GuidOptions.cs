namespace Telegram_WetterOnline_Bot.Classes
{
    public class GuidOptions
    {
        public static Guid GetGuid()
        {
            Logger.Log(Logger.LogLevel.Debug,"GuidOptions", $"A GUID was requestet!  {DateTime.Now}");   
            //creates and returns a new GUID
            return Guid.NewGuid();
        }
    }
}
