using System.Collections.Concurrent;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Solutions.Magic
{
    public abstract class TestBase
    {
        private static readonly ConcurrentDictionary<string, IWebDriver> AcquiredWebDrivers = new ConcurrentDictionary<string, IWebDriver>();
        private static string TestWorkerId => TestContext.CurrentContext.WorkerId;

        [TearDown]
        public void TearDown()
        {
            if (AcquiredWebDrivers.TryRemove(TestWorkerId, out var webDriver))
            {
                AssemblySetUpFixture.WebDriverPool.Release(webDriver);
            }
        }

        protected static IWebDriver GetWebDriver()
        {
            return AcquiredWebDrivers.GetOrAdd(TestWorkerId, x => AssemblySetUpFixture.WebDriverPool.Acquire());
        }
    }
}