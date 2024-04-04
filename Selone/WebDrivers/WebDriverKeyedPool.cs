using System;
using System.Collections.Concurrent;
using OpenQA.Selenium;

namespace Kontur.Selone.WebDrivers
{
    public class WebDriverKeyedPool<TKey> : IWebDriverKeyedPool<TKey>
    {
        private readonly ConcurrentDictionary<TKey, IWebDriverPool> pools = new ConcurrentDictionary<TKey, IWebDriverPool>();
        private readonly ConcurrentDictionary<IWebDriver, TKey> acquired = new ConcurrentDictionary<IWebDriver, TKey>();

        public IWebDriverKeyedPool<TKey> Register(TKey key, IWebDriverFactory factory, IWebDriverCleaner cleaner, IWebDriverDisposer disposer)
        {
            if (!pools.TryAdd(key, new WebDriverPool(factory, cleaner, disposer)))
            {
                throw new Exception($"WebDriverFactory for key '{key}' already registered");
            }

            return this;
        }

        public IWebDriver Acquire(TKey key)
        {
            return AcquireInternal(key);
        }

        public IPooledWebDriver AcquireWrapper(TKey key)
        {
            return new PooledWebDriver(AcquireInternal(key), ReleaseInternal);
        }

        public void Release(IWebDriver webDriver)
        {
            ReleaseInternal(webDriver);
        }

        public void Clear()
        {
            foreach (var pool in pools)
            {
                pool.Value.Clear();
            }
        }

        private IWebDriver AcquireInternal(TKey key)
        {
            var pool = GetPool(key);
            var webDriver = pool.Acquire();
            acquired.TryAdd(webDriver, key);
            return webDriver;
        }

        private void ReleaseInternal(IWebDriver webDriver)
        {
            if (!acquired.TryRemove(webDriver, out var key))
            {
                throw new Exception($"WebDriver {webDriver.GetType().Name} was not taken from the pool or already released");
            }

            GetPool(key).Release(webDriver);
        }

        private IWebDriverPool GetPool(TKey key)
        {
            if (!pools.TryGetValue(key, out var pool))
            {
                throw new Exception($"WebDriverFactory for key '{key}' not registered");
            }

            return pool;
        }
    }
}