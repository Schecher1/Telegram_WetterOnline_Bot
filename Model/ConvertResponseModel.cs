namespace Telegram_WetterOnline_Bot.Model
{
    public class ConvertResponseModel
    {
        public int ConversionCost { get; set; }
        public FileRes[] Files { get; set; }
    }
    public class FileRes
    {
        public string FileName { get; set; }
        public string FileExt { get; set; }
        public int FileSize { get; set; }
        public string FileId { get; set; }
        public string Url { get; set; }
    }
}
