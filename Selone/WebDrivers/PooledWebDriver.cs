using System;
using OpenQA.Selenium;

namespace Kontur.Selone.WebDrivers
{
    public class PooledWebDriver : IPooledWebDriver
    {
        private readonly Action<IWebDriver> onDispose;

        public PooledWebDriver(IWebDriver webDriver, Action<IWebDriver> onDispose)
        {
            this.onDispose = onDispose;
            WrappedDriver = webDriver;
        }

        public IWebDriver WrappedDriver { get; }

        public void Dispose()
        {
            onDispose?.Invoke(WrappedDriver);
        }
    }
}