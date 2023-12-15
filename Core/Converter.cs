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
                    DeviceScaleFactor = 4,
                });

                //goto a link
                await page.GoToAsync(widgetUrl);
                await page.WaitForNetworkIdleAsync();

                // Your JavaScript code
                string script = @"
                //move the background image to the div element (render porpose)
                const aElement = document.querySelector('[rel=""nofollow""]');
                const divElement = document.querySelector('div.content');
                const backgroundImageUrl = aElement.style.backgroundImage;
                divElement.style.backgroundImage = backgroundImageUrl;

                 //remove the arrow element on the right side (optic porpose)
                const footerElement = document.getElementsByClassName('arrow');
                footerElement[0].remove();

                //add a watermark to the image (just for fun) 
                const footer = document.querySelector('footer');
                footer.textContent = 'GitHub: Schecher1/Telegram_WetterOnline_Bot';
                footer.style = 'margin-top:5px;';";


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
