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

        public static void CloseRedundantWindows(this IWebDriver driver)
        {
            if (driver.WindowHandles.Count > 1)
            {
                foreach (var handle in driver.WindowHandles.Skip(1))
                {
                    driver.SwitchTo().Window(handle).Close();
                }

                driver.SwitchTo().Window(driver.WindowHandles.Single());
            }

            driver.Navigate().GoToUrl("about:blank");
        }

        public static ITakesScreenshot Screenshoter(this IWebDriver webDriver)
        {
            return (ITakesScreenshot) webDriver;
        }
    }
}