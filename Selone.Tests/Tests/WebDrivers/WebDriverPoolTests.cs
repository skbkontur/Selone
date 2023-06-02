using System.Threading;
using Kontur.Selone.Extensions;
using Kontur.Selone.Tests.Browsers;
using Kontur.Selone.WebDrivers;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Kontur.Selone.Tests.Tests.WebDrivers
{
    public class WebDriverPoolTests
    {
        [Test]
        public void Test()
        {
            var webDriverPool = new WebDriverPool(BrowserPool.ChromeDriverFactory, new DelegateWebDriverCleaner(x => x.ResetWindows()));
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

        [Test]
        public void Create_New_WebDriver_Instance_When_Previous_Was_Disposed()
        {
            var webDriverPool = new WebDriverPool(BrowserPool.ChromeDriverFactory, new DelegateWebDriverCleaner(x =>
            {
                x.ResetWindows();
                x.Quit();
            }));
            
            var webDriver = webDriverPool.Acquire();
            var sessionId1 = ((IHasSessionId)webDriver).SessionId;
            webDriverPool.Release(webDriver);

            webDriver = webDriverPool.Acquire();
            var sessionId2 = ((IHasSessionId)webDriver).SessionId;
            webDriverPool.Release(webDriver);
            
            Assert.AreNotEqual(sessionId1, sessionId2);
            
            webDriverPool.Clear();
        }
    }
}