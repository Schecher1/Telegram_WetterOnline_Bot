namespace Telegram_WetterOnline_Bot.Classes
{
    public class EnvironmentVariable
    {
        public static readonly string SYSTEM_VERSION = "1.5.0.5";
        public static List<LogModel> SYSTEM_LOG { get; set; } = new List<LogModel>();
        public static bool SYSTEM_LogTerminalIsOpen { get; set; } = false;
        public static string TELEGRAM_API_TOKEN { get; set; } = String.Empty;
        public static string CONVERT_API_TOKEN { get; set; } = String.Empty;
        public static string TELEGRAMBOT_OWNER_NAME { get; set; } = String.Empty;
        public static int[] TELEGRAM_ID_WHITELIST { get; set; } = new int[0];
        public static readonly string WETTERONLINE_API_HOST = @"https://api.wetteronline.de/";
        public static readonly string WETTERONLINE_API_LANGUAGE = "de";
        public static readonly string WETTERONLINE_API_FORMAT = "json";
        public static readonly string WETTERONLINE_API_LAYOUT = "FC3";
        public static readonly string CONVERT_API_HOST = @$"https://v2.convertapi.com/convert/html/to/png?Secret=";
    }
}
