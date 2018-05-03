using System;
using Kontur.Selone.WebDrivers;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;

namespace Kontur.Selone.Tests.Browsers.Factories
{
    public class InternetExplorerDriverFactory : IWebDriverFactory
    {
        //todo автоматизировать https://selenium2.ru/component/content/article.html?id=111
        private readonly InternetExplorerDriverFactoryConfiguration configuration;

        public InternetExplorerDriverFactory(InternetExplorerDriverFactoryConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IWebDriver Create()
        {
            var service = CreateChromeDriverService();
            var options = CreateChromeOptions();
            return CreateDriver(service, options);
        }

        private static InternetExplorerOptions CreateChromeOptions()
        {
            return new InternetExplorerOptions
            {
                //устанавливаем не дефолтное условие загрузки страницы, из-за проблемы http://barancev.github.io/slow-loading-pages/
                PageLoadStrategy = PageLoadStrategy.None
            };
        }

        private static IWebDriver CreateDriver(InternetExplorerDriverService service, InternetExplorerOptions options)
        {
            //commandTimeout увеличен с дефолтных 60 до 180 секунд, т.к. при 60 время от времени получаем таймаут
            //подробнее тут: https://github.com/SeleniumHQ/selenium/wiki/DotNet-Bindings
            var internetExplorerDriver = new InternetExplorerDriver(service, options, TimeSpan.FromSeconds(180));
            //internetExplorerDriver.Manage().Window.SetSize(configuration.WindowSize.Width, configuration.WindowSize.Height);
            return internetExplorerDriver;
        }

        private InternetExplorerDriverService CreateChromeDriverService()
        {
            return InternetExplorerDriverService.CreateDefaultService(configuration.DriverDirectoryPath);
        }
    }
}