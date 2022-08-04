namespace Telegram_WetterOnline_Bot.Model
{
    public class ConfigModel
    {
        public string Telegram_API_Token { get; set; } = String.Empty;
        public string Convert_API_Token { get; set; } = String.Empty;
        public string Telegrambot_Owner_Name { get; set; } = String.Empty;
        public int[] Telegrambot_ID_Whitelist { get; set; } = new int[0];
    }
}