using System.Linq;
using System.Threading;
using Kontur.Selone.Extensions;
using Kontur.Selone.Tests.Browsers;
using Kontur.Selone.Tests.Browsers.Factories;
using Kontur.Selone.WebDrivers;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Kontur.Selone.Tests.Tests.WebDrivers
{
    public class WebDriverPoolTests
    {
        [Test]
        public void Test()
        {
            var webDriverPool = new WebDriverPool(BrowserPool.ChromeDriverFactory,
                new DelegateWebDriverCleaner(x => x.ResetWindows()), BrowserPool.DriverDisposer);
            using (var pooled = webDriverPool.AcquireWrapper())
            {
                var webDriver = pooled.WrappedDriver;
                webDriver.Navigate().GoToUrl("https://google.com");
                Thread.Sleep(1000);
            }

            Thread.Sleep(1000);
            using (var pooled = webDriverPool.AcquireWrapper())
            {
                var webDriver = pooled.WrappedDriver;
                Assert.That(webDriver.Url, Is.EqualTo("about:blank"));
                webDriver.Navigate().GoToUrl("https://google.com");
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
            }), BrowserPool.DriverDisposer);

            var webDriver = webDriverPool.Acquire();
            var sessionId1 = ((IHasSessionId) webDriver).SessionId;
            webDriverPool.Release(webDriver);

            webDriver = webDriverPool.Acquire();
            var sessionId2 = ((IHasSessionId) webDriver).SessionId;
            webDriverPool.Release(webDriver);

            Assert.AreNotEqual(sessionId1, sessionId2);

            webDriverPool.Clear();
        }

        private IWebDriverPool InitWebDriverPool()
        {
            using var sp = new ServiceCollection()
                .AddSingleton<IWebDriverPool, WebDriverPool>()
                .AddSingleton<IWebDriverFactory, ChromeDriverFactory>()
                .AddSingleton<ChromeDriverFactoryConfiguration>()
                .AddSingleton<IWebDriverCleaner>(_ => new DelegateWebDriverCleaner(x => x.ResetWindows()))
                .AddSingleton<IWebDriverDisposer, WebDriverDisposer>(_ => new WebDriverDisposer(x =>
                {
                    x.Close();
                    x.Quit();
                    x.Dispose();
                })).BuildServiceProvider();

            return sp.GetRequiredService<IWebDriverPool>();
        }
        
        [Test]
        public void CorrectReusedDrivers_AndDispose()
        {
            var driverPool = InitWebDriverPool();

            var firstUsingDrivers = new[] {driverPool.Acquire(), driverPool.Acquire()};
            foreach (var driver in firstUsingDrivers)
            {
                driver.Navigate().GoToUrl("https://ya.ru");
                driverPool.Release(driver);
            }

            var secondUsingDrivers = new[] {driverPool.Acquire(), driverPool.Acquire()};
            foreach (var driver in secondUsingDrivers)
            {
                driver.Navigate().GoToUrl("https://ya.ru/pogoda");
                driverPool.Release(driver);
            }

            Assert.Multiple(() =>
            {
                Assert.That(
                    firstUsingDrivers.Select(x => ((IHasSessionId) x).SessionId),
                    Is.EquivalentTo(
                        secondUsingDrivers.Select(x => ((IHasSessionId) x).SessionId))
                );
                Assert.DoesNotThrow(() => driverPool.Clear());
                Assert.IsTrue(firstUsingDrivers.All(x=> ((IHasSessionId) x).SessionId is null));
            });
        }
    }
}