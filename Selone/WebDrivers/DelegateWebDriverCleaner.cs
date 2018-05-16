using System;
using OpenQA.Selenium;

namespace Kontur.Selone.WebDrivers
{
    public class DelegateWebDriverCleaner : IWebDriverCleaner
    {
        private readonly Action<IWebDriver> clearAction;

        public DelegateWebDriverCleaner(Action<IWebDriver> clearAction)
        {
            this.clearAction = clearAction;
        }

        public void Clear(IWebDriver webDriver)
        {
            clearAction?.Invoke(webDriver);
        }
    }
}