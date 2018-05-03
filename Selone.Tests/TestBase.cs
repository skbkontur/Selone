using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Kontur.Selone.Extensions;
using Kontur.Selone.Helpers;
using Kontur.Selone.Tests.Browsers;
using Kontur.Selone.Tests.Extensions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;

namespace Kontur.Selone.Tests
{
    [TestFixture]
    public abstract class TestBase
    {
        private static readonly ConcurrentDictionary<string, List<IWebDriver>> acquiredWebDrivers = new ConcurrentDictionary<string, List<IWebDriver>>();

        [SetUp]
        public virtual void SetUp()
        {
        }

        [TearDown]
        public virtual void TearDown()
        {
            if (!acquiredWebDrivers.TryRemove(TestContext.CurrentContext.WorkerId, out var webDrivers))
            {
                return;
            }

            var webDriver = webDrivers.Single();

            try
            {
                try
                {
                    webDriver.AssertClientErrorsAbsent();
                }
                finally
                {
                    if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
                    {
                        PublishClientDebugLogs(webDriver);
                        PublichScreenshot(webDriver, @"C:\temp\.screnshot", x => x);
                    }
                }
            }
            finally
            {
                BrowserPool.Instance.Release(webDriver);
            }
        }

        protected IWebDriver Acquire(Browser browser)
        {
            var webDriver = BrowserPool.Instance.Acquire(browser);
            acquiredWebDrivers.GetOrAdd(TestContext.CurrentContext.WorkerId, x => new List<IWebDriver>()).Add(webDriver);
            return webDriver;
        }

        private static void PublishClientDebugLogs(IWebDriver webDriver)
        {
            try
            {
                var logs = webDriver.GetClientDebugLogs();
                if (string.IsNullOrWhiteSpace(logs))
                {
                    Console.WriteLine("No client debug logs");
                }
                else
                {
                    Console.WriteLine("Client debug logs:");
                    Console.WriteLine("------------------");
                    Console.WriteLine(logs);
                    Console.WriteLine("------------------");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Can't get client debug logs: {e}");
            }
        }

        private static void PublichScreenshot(IWebDriver webDriver, string dir, Func<string, string> transformTestName)
        {
            try
            {
                var testName = transformTestName(TestContext.CurrentContext.Test.FullName);
                webDriver.Screenshoter().GetScreenshot().Save(dir, testName);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Can't get screenshot: {e}");
            }
        }
    }
}