namespace Telegram_WetterOnline_Bot.Model
{
    public class LocationModel
    {
        public string? locationName { get; set; }
        public object? subLocationName { get; set; }
        public object? server { get; set; }
        public object? serverKey { get; set; }
        public string? geoID { get; set; }
        public string? geoName { get; set; }
        public string? zipCode { get; set; }
        public string? subStateID { get; set; }
        public string? subStateName { get; set; }
        public string? stateID { get; set; }
        public string? stateName { get; set; }
        public float? latitude { get; set; }
        public float? longitude { get; set; }
        public int? altitude { get; set; }
        public int? utcOffset { get; set; }
        public string? timeZone { get; set; }
        public string? url { get; set; }
        public string? match { get; set; }
    }
}