using System;
using System.Diagnostics;
using System.IO;
using Kontur.Selone.Extensions;
using Kontur.Selone.WebDrivers;
using Microsoft.Win32;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Kontur.Selone.Tests.Browsers.Factories
{
    public class ChromeDriverFactory : IWebDriverFactory
    {
        private const string RegistryKey = @"Software\Google\Update\Clients\{8A69D345-D564-463c-AFF1-A69D9E530F96}";
        private readonly ChromeDriverFactoryConfiguration configuration;
        private readonly string chromeExe;

        public ChromeDriverFactory(ChromeDriverFactoryConfiguration configuration)
        {
            this.configuration = configuration;
            chromeExe = Path.Combine(configuration.ChromeDirectoryPath, "chrome.exe");
        }

        public IWebDriver Create()
        {
            SetChromeVersionToRegistry(chromeExe);
            for (var i = 0; i < 3; i++)
            {
                try
                {
                    var chromeDriverService = CreateChromeDriverService();
                    var options = CreateChromeOptions(chromeExe);
                    return CreateChromeDriver(chromeDriverService, options);
                }
                //ошибка возникает периодически
                //предложение: перейти на новую версию chromedriver (сейчас 2.24, chrome - 53)
                //но в настоящий момент при переходе на новую версию драйвера возможны ошибки другого рода
                //решение: подождать стабилизации новой версии, после чего перейти на нее и удалить ретраи
                catch (InvalidOperationException e) when (e.Message.Contains("session not created exception"))
                {
                }
            }

            return null;
        }

        private static void SetChromeVersionToRegistry(string chromeExe)
        {
            var key = Registry.CurrentUser.CreateSubKey(RegistryKey);
            if (key == null)
            {
                throw new Exception($"Не удалось создать ключ {Registry.CurrentUser}\\{RegistryKey}");
            }

            key.SetValue("pv", FileVersionInfo.GetVersionInfo(chromeExe).ProductVersion);
            key.Close();
        }

        private static ChromeOptions CreateChromeOptions(string chromeExe)
        {
            //"--no-sandbox" добавлен, потому что столкнулись с проблемой: Chrome падает сразу же после запуска
            //подробнее тут: https://sites.google.com/a/chromium.org/chromedriver/help/chrome-doesn-t-start
            var options = new ChromeOptions {BinaryLocation = chromeExe};
            options.AddArguments("--no-sandbox", "--start-maximized", "--disable-extensions");
            return options;
        }

        private ChromeDriverService CreateChromeDriverService()
        {
            return ChromeDriverService.CreateDefaultService(configuration.DriverDirectoryPath);
        }

        private IWebDriver CreateChromeDriver(ChromeDriverService chromeDriverService, ChromeOptions options)
        {
            //commandTimeout увеличен с дефолтных 60 до 180 секунд, т.к. при 60 время от времени получаем таймаут
            //подробнее тут: https://github.com/SeleniumHQ/selenium/wiki/DotNet-Bindings
            var chromeDriver = new ChromeDriver(chromeDriverService, options, TimeSpan.FromSeconds(180));
            chromeDriver.Manage().Window.SetSize(configuration.WindowSize.Width, configuration.WindowSize.Height);
            return chromeDriver;
        }
    }
}