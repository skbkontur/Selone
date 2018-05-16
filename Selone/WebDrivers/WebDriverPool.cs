using System;
using System.Collections.Concurrent;
using OpenQA.Selenium;

namespace Kontur.Selone.WebDrivers
{
    public class WebDriverPool : IWebDriverPool
    {
        private readonly IWebDriverFactory factory;
        private readonly IWebDriverCleaner cleaner;
        private readonly ConcurrentQueue<IWebDriver> queue = new ConcurrentQueue<IWebDriver>();
        private readonly ConcurrentDictionary<IWebDriver, bool> acquired = new ConcurrentDictionary<IWebDriver, bool>();

        public WebDriverPool(IWebDriverFactory factory, IWebDriverCleaner cleaner)
        {
            this.factory = factory;
            this.cleaner = cleaner;
        }

        public IWebDriver Acquire()
        {
            return AcquireInternal();
        }

        public IPooledWebDriver AcquireWrapper()
        {
            return new PooledWebDriver(AcquireInternal(), ReleaseInternal);
        }

        public void Release(IWebDriver webDriver)
        {
            ReleaseInternal(webDriver);
        }

        public void Clear()
        {
            while (queue.TryDequeue(out var webDriver))
            {
                webDriver.Dispose();
            }
        }

        private IWebDriver AcquireInternal()
        {
            var webDriver = queue.TryDequeue(out var existing) ? existing : factory.Create();
            acquired.TryAdd(webDriver, true);
            return webDriver;
        }

        private void ReleaseInternal(IWebDriver webDriver)
        {
            if (!acquired.TryRemove(webDriver, out var dummy))
            {
                throw new Exception($"WebDriver {webDriver.GetType().Name} was not taken from the pool or already released");
            }

            cleaner?.Clear(webDriver);
            queue.Enqueue(webDriver);
        }
    }
}