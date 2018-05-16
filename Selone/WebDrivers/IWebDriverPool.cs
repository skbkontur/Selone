using OpenQA.Selenium;

namespace Kontur.Selone.WebDrivers
{
    public interface IWebDriverPool
    {
        IWebDriver Acquire();
        void Release(IWebDriver webDriver);
        IPooledWebDriver AcquireWrapper();
        void Clear();
    }
}