using System.Net;
using System.Text;

namespace Telegram_WetterOnline_Bot.Core
{
    public class ConvertApi
    {
        public static string HtmlToPng(string source)
        {
            WebClient wc = new WebClient();
            
            string returnValue = String.Empty;

            string widgetHtml = wc.DownloadString(source);

            string widgetBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(widgetHtml));

            HttpClient client = new HttpClient();

            string param = GetConvertApiParams(Guid.NewGuid().ToString(), widgetBase64);

            DateTime start = DateTime.Now;
            Logger.Log(Logger.LogLevel.Debug, "Converter", $"Html to Png was requestet!   START ");

            using (var content = new StringContent(param, System.Text.Encoding.UTF8, "application/json"))
            {
                HttpResponseMessage result = client.PostAsync(EnvironmentVariable.CONVERT_API_HOST + EnvironmentVariable.CONVERT_API_TOKEN, content).Result;
                returnValue = result.Content.ReadAsStringAsync().Result;
            }
            Logger.Log(Logger.LogLevel.Debug, "Converter", $"Html to Png was requestet!   END     Duration:{(DateTime.Now - start).TotalSeconds} seconds");

            if (returnValue.Contains("User credentials not set, secret or token must be passed."))
            {
                Logger.Log(Logger.LogLevel.Error, "Converter", $"Html to Png was requestet!   ERROR: {returnValue}");
                return String.Empty;
            }
            
            return JsonConvert.DeserializeObject<ConvertResponseModel>(returnValue).Files[^1].Url;
        }

        private static string GetConvertApiParams(string filename, string widgetBase64)
        {
            //Crappy as hell but i works
            return "{\"Parameters\":[{\"Name\":\"File\",\"FileValue\":{\"Name\":\"my_file.html\",\"Data\":\"" + widgetBase64 + "\"}},{\"Name\":\"StoreFile\",\"Value\":true},{\"Name\":\"FileName\",\"Value\":\"" + filename + "\"},{\"Name\":\"Zoom\",\"Value\":\"10\"},{\"Name\":\"ImageWidth\",\"Value\":\"300\"},{\"Name\":\"ImageHeight\",\"Value\":\"365\"}]}";
        }
    }
}
