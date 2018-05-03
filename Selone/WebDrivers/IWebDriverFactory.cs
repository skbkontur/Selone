using OpenQA.Selenium;

namespace Kontur.Selone.WebDrivers
{
    public interface IWebDriverFactory
    {
        IWebDriver Create();
    }
}