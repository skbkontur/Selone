using System;
using System.Diagnostics;
using System.IO;
using Kontur.Selone.Extensions;
using Kontur.Selone.WebDrivers;
using Microsoft.Win32;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Solutions.Magic
{
    public class ChromeDriverFactory : IWebDriverFactory
    {
        private static readonly string Path = System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", ".."));

        public IWebDriver Create()
        {

            for (var i = 0; i < 3; i++)
            {
                try
                {
                    var chromeDriverService = CreateChromeDriverService();
                    var chromeDriver = new ChromeDriver(chromeDriverService);
                    return chromeDriver;
                    
                }
                catch (InvalidOperationException e) when (e.Message.Contains("session not created exception"))
                {
                }
            }

            return null;
        }
        
        private ChromeDriverService CreateChromeDriverService()
        {
            return ChromeDriverService.CreateDefaultService(Path);
        }
    }
}