namespace Telegram_WetterOnline_Bot.Classes
{
    public class HttpRequestManager
    {
        //only for the wetteronline api, not for the other apis!
        public static string HttpGet(string query)
        {
            Logger.Log(Logger.LogLevel.Debug, "HttpRequestManager", $"Http Request Query: {query}");

            HttpClient client = new();

            //Create the Uri for the request
            var entpoint = new Uri(EnvironmentVariable.WETTERONLINE_API_HOST + query);

            //Makes the Get and saves the Result
            var result = client.GetAsync(entpoint).Result;

            //Take only the Result, without the other data
            var json = result.Content.ReadAsStringAsync().Result;

            Logger.Log(Logger.LogLevel.Debug, "HttpRequestManager", $"Http Request  result: {json}");

            //Return the result
            return json;
        }
    }
}
