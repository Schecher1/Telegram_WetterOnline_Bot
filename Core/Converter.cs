using PuppeteerSharp;

namespace Telegram_WetterOnline_Bot.Core
{
    public class Converter
    {
        public static async Task<string> HtmlToJpeg(string widgetUrl)
        {
            string outputPath = GetTempFilePath();

            var launchOptions = new LaunchOptions
            {
                Headless = true,
                ExecutablePath = "/usr/bin/google-chrome-stable",
                Args = new[] { "--no-sandbox" }
            };

            await new BrowserFetcher().DownloadAsync();
            using (var browser = await Puppeteer.LaunchAsync(launchOptions))
            using (var page = await browser.NewPageAsync())
            {
                await page.SetViewportAsync(new ViewPortOptions
                {
                    DeviceScaleFactor = 3
                });

                //goto a link
                await page.GoToAsync(widgetUrl);

                // Your JavaScript code
                var script = @"
                var aElement = document.querySelector('[rel=""nofollow""]');
                var divElement = document.querySelector('div.content');
                var backgroundImageUrl = aElement.style.backgroundImage;
                divElement.style.backgroundImage = backgroundImageUrl;
                ";

                // Evaluate the script on the page
                await page.EvaluateExpressionAsync(script);
               
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
            string tempDirectory = Path.GetTempPath();
            string tempFileName = Path.GetRandomFileName() + ".jpeg";
            string tempFilePath = Path.Combine(tempDirectory, tempFileName);
            return tempFilePath;
        }
    }
}
