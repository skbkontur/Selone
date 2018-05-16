using OpenQA.Selenium;

namespace Kontur.Selone.WebDrivers
{
    public interface IWebDriverCleaner
    {
        void Clear(IWebDriver webDriver);
    }
}