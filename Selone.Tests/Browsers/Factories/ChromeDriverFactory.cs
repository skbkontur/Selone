using Kontur.Selone.Extensions;
using Kontur.Selone.WebDrivers;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Kontur.Selone.Tests.Browsers.Factories
{
    public class ChromeDriverFactory : IWebDriverFactory
    {
        private readonly ChromeDriverFactoryConfiguration configuration;

        public ChromeDriverFactory(ChromeDriverFactoryConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IWebDriver Create()
        {
            var chromeDriverService = CreateChromeDriverService();
            var options = CreateChromeOptions();
            return CreateChromeDriver(chromeDriverService, options);
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

        private IWebDriver CreateChromeDriver(ChromeDriverService chromeDriverService, ChromeOptions options)
        {
            var chromeDriver = new ChromeDriver(chromeDriverService, options);
            chromeDriver.Manage().Window.SetSize(configuration.WindowSize.Width, configuration.WindowSize.Height);
            return chromeDriver;
        }
    }
}