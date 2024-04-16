using System.Linq;
using OpenQA.Selenium;

namespace Kontur.Selone.Extensions
{
    public static class WebDriverExtensions
    {
        public static IJavaScriptExecutor JavaScriptExecutor(this IWebDriver webDriver)
        {
            return (IJavaScriptExecutor) webDriver;
        }

        public static IWebDriver ResetWindows(this IWebDriver driver)
        {
            var current = driver.CurrentWindowHandle;
            foreach (var handle in driver.WindowHandles.Where(x => x != current))
            {
                driver.SwitchTo().Window(handle).Close();
            }

            driver.SwitchTo().Window(current);
            driver.Navigate().GoToUrl("about:blank");
            return driver;
        }

        public static string OpenWindow(this IWebDriver driver)
        {
            var initialHandles = driver.WindowHandles;
            driver.JavaScriptExecutor().ExecuteScript("window.open()");
            return driver.WindowHandles.Except(initialHandles).Single();
        }

        public static IWebDriver SwitchToNewWindow(this IWebDriver driver)
        {
            var windowHandle = driver.OpenWindow();
            return driver.SwitchTo().Window(windowHandle);
        }

        public static ITakesScreenshot Screenshoter(this IWebDriver webDriver)
        {
            return (ITakesScreenshot) webDriver;
        }
    }
}