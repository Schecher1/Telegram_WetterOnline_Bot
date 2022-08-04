namespace Telegram_WetterOnline_Bot.Core
{
    public class WetterOnline
    {
        public static List<AutoSuggestModel>? GetSuggestData(string locationName)
        {
            Logger.Log(Logger.LogLevel.Debug, "WetterOnlineAPI", $"Get Suggest Data from {locationName}");

            //Get the rawJson from the API
            string rawJson = HttpRequestManager.HttpGet(
                            $"autosuggest?name={locationName}" +
                            $"&lang={EnvironmentVariable.WETTERONLINE_API_LANGUAGE}" +
                            $"&format={EnvironmentVariable.WETTERONLINE_API_FORMAT}");

            //Checks if the Response is not failed
            if (rawJson == "[]")
                return null;

            //Deserialize the edited json into the SuggestDataModel
            List<AutoSuggestModel>? suggestData = JsonConvert.DeserializeObject<List<AutoSuggestModel>>(rawJson);

            //Return the suggestData
            return suggestData;
        }

        public static LocationModel? GetLocationData(string locationName)
        {
            Logger.Log(Logger.LogLevel.Debug, "WetterOnlineAPI", $"Get Location Data from {locationName}");

            //Get the rawJson from the API
            string rawJson = HttpRequestManager.HttpGet(
                            $"search?name={locationName}" +
                            $"&lang={EnvironmentVariable.WETTERONLINE_API_LANGUAGE}" +
                            $"&function=url" +
                            $"&v=1" +
                            $"&format={EnvironmentVariable.WETTERONLINE_API_FORMAT}");

            //Checks if the Response is not failed or something was found
            if (rawJson == "[{\"match\":\"no\"}]")
                return null;

            //Deserialize the edited json into the LocationModel
            List<LocationModel>? locationData = JsonConvert.DeserializeObject<List<LocationModel>>(rawJson);

            //Return the suggestData
            return locationData[0];
        }

        public static string GetWidgetLink(string gid, string locationname)
        {
           Logger.Log(Logger.LogLevel.Debug, "WetterOnlineAPI", $"Get Widget-Link from {locationname} ({gid})");

            //Create the API-Link
            string link = $"{EnvironmentVariable.WETTERONLINE_API_HOST}" +
                          $"wetterwidget?gid={gid}" +
                          $"&modeid={EnvironmentVariable.WETTERONLINE_API_LAYOUT}" +
                          $"&locationname={locationname}" +
                          $"&lang={EnvironmentVariable.WETTERONLINE_API_LANGUAGE}";

            //Return the widgetData
            return link;
        }
    }
}
