using System.Threading;
using Kontur.Selone.Tests.Browsers;
using Kontur.Selone.WebDrivers;
using NUnit.Framework;

namespace Kontur.Selone.Tests.Tests.WebDrivers
{
    public class WebDriverPoolTests
    {
        [Test]
        public void Test()
        {
            var webDriverPool = new WebDriverPool(BrowserPool.ChromeDriverFactory);
            using (var pooled = webDriverPool.AcquireWrapper())
            {
                var webDriver = pooled.WrappedDriver;
                webDriver.Navigate().GoToUrl("http://google.com");
                Thread.Sleep(1000);
            }

            Thread.Sleep(1000);
            using (var pooled = webDriverPool.AcquireWrapper())
            {
                var webDriver = pooled.WrappedDriver;
                Assert.That(webDriver.Url, Is.EqualTo("about:blank"));
                webDriver.Navigate().GoToUrl("http://google.com");
                Thread.Sleep(1000);
            }

            webDriverPool.Clear();
        }
    }
}