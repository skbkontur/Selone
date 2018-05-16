using OpenQA.Selenium;

namespace Kontur.Selone.WebDrivers
{
    public interface IWebDriverPools<TKey>
    {
        WebDriverPools<TKey> Register(TKey key, IWebDriverFactory factory, IWebDriverCleaner cleaner);
        IWebDriver Acquire(TKey key);
        IPooledWebDriver AcquireWrapper(TKey key);
        void Release(IWebDriver webDriver);
        void Clear();
    }
}