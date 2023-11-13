using PuppeteerSharp;

namespace Telegram_WetterOnline_Bot.Core
{
    public class Converter
    {
        public static async Task<string> HtmlToJpeg(string widgetUrl)
        {
            string outputPath = GetTempFilePath();

            await new BrowserFetcher().DownloadAsync();
            using (var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true }))
            using (var page = await browser.NewPageAsync())
            {
                await page.SetViewportAsync(new ViewPortOptions
                {
                    DeviceScaleFactor = 3
                });

                //goto page
                await page.GoToAsync(widgetUrl);

                //set screenshot options
                var screenshotOptions = new ScreenshotOptions
                {
                    Type = ScreenshotType.Jpeg,
                    FullPage = false,
                    Clip = new()
                    {
                        X = 250,
                        Y = 150,
                        Width = 300, 
                        Height = 300
                    },
                    Quality = 100
                };

                await page.ScreenshotAsync(outputPath, screenshotOptions);
            }

            return outputPath;
        }

        private static string GetTempFilePath()
        {
            string tempDirectory = System.IO.Path.GetTempPath();
            string tempFileName = System.IO.Path.GetRandomFileName() + ".jpeg";
            string tempFilePath = System.IO.Path.Combine(tempDirectory, tempFileName);
            return tempFilePath;
        }
    }
}
