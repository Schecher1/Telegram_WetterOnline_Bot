namespace Telegram_WetterOnline_Bot.Classes
{
    public class EnvironmentVariable
    {
        public static readonly string SYSTEM_VERSION = "2.2.1.0";
        public static string TELEGRAM_API_TOKEN { get; set; } = String.Empty;
        public static string TELEGRAMBOT_OWNER_NAME { get; set; } = String.Empty;
        public static int[] TELEGRAM_ID_WHITELIST { get; set; } = new int[0];
        public static bool IS_DEBUG { get; set; }
        public static readonly string WETTERONLINE_API_HOST = @"https://api.wetteronline.de/";
        public static readonly string WETTERONLINE_API_LANGUAGE = "de";
        public static readonly string WETTERONLINE_API_FORMAT = "json";
        public static readonly string WETTERONLINE_API_LAYOUT = "FC3";
    }
}
