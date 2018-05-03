using System;
using System.Collections.Concurrent;
using OpenQA.Selenium;

namespace Kontur.Selone.WebDrivers
{
    public class WebDriverPools<TKey> : IWebDriverPools<TKey>
    {
        private readonly ConcurrentDictionary<TKey, IWebDriverPool> pools = new ConcurrentDictionary<TKey, IWebDriverPool>();
        private readonly ConcurrentDictionary<IWebDriver, TKey> acquired = new ConcurrentDictionary<IWebDriver, TKey>();

        public WebDriverPools<TKey> Register(TKey key, IWebDriverFactory factory)
        {
            if (!pools.TryAdd(key, new WebDriverPool(factory)))
            {
                throw new Exception($"WebDriverFactory for key '{key}' already registered");
            }

            return this;
        }

        public IWebDriver Acquire(TKey key)
        {
            var pool = GetPool(key);
            var webDriver = pool.Acquire();
            acquired.TryAdd(webDriver, key);
            return webDriver;
        }

        public void Release(IWebDriver webDriver)
        {
            if (!acquired.TryRemove(webDriver, out var key))
            {
                throw new Exception($"WebDriver {webDriver.GetType().Name} was not taken from the pool or already released");
            }

            GetPool(key).Release(webDriver);
        }

        public void Clear()
        {
            foreach (var pool in pools)
            {
                pool.Value.Clear();
            }
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