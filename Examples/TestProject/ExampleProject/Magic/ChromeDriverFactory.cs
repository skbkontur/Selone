using Kontur.Selone.WebDrivers;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Solutions.Magic
{
    public class ChromeDriverFactory : IWebDriverFactory
    {
        public IWebDriver Create()
        {
            var chromeDriverService = CreateChromeDriverService();
            var options = CreateChromeOptions();
            var chromeDriver = new ChromeDriver(chromeDriverService, options);
            return chromeDriver;
        }

        private static ChromeOptions CreateChromeOptions()
        {
            var options = new ChromeOptions();
            return options;
        }

        private ChromeDriverService CreateChromeDriverService()
        {
            return ChromeDriverService.CreateDefaultService();
        }
    }
}