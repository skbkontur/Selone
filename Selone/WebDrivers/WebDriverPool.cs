using System;
using System.Collections.Concurrent;
using Kontur.Selone.Extensions;
using OpenQA.Selenium;

namespace Kontur.Selone.WebDrivers
{
    public class WebDriverPool : IWebDriverPool
    {
        private readonly IWebDriverFactory factory;
        private readonly ConcurrentQueue<IWebDriver> queue = new ConcurrentQueue<IWebDriver>();
        private readonly ConcurrentDictionary<IWebDriver, bool> acquired = new ConcurrentDictionary<IWebDriver, bool>();

        public WebDriverPool(IWebDriverFactory factory)
        {
            this.factory = factory;
        }

        public IWebDriver Acquire()
        {
            var webDriver = queue.TryDequeue(out var existing) ? existing : factory.Create();
            acquired.TryAdd(webDriver, true);
            return webDriver;
        }

        public void Release(IWebDriver webDriver)
        {
            if (!acquired.TryRemove(webDriver, out var dummy))
            {
                throw new Exception($"WebDriver {webDriver.GetType().Name} was not taken from the pool or already released");
            }

            webDriver.CloseRedundantWindows();
            queue.Enqueue(webDriver);
        }

        public void Clear()
        {
            while (queue.TryDequeue(out var webDriver))
            {
                webDriver.Dispose();
            }
        }
    }
}