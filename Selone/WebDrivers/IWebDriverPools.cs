using OpenQA.Selenium;

namespace Kontur.Selone.WebDrivers
{
    public interface IWebDriverPools<TKey>
    {
        WebDriverPools<TKey> Register(TKey key, IWebDriverFactory factory);
        IWebDriver Acquire(TKey key);
        void Release(IWebDriver webDriver);
        void Clear();
    }
}