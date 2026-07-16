using Kontur.Selone.Extensions;
using Kontur.Selone.Tests.Browsers;
using Kontur.Selone.Tests.Extensions;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Kontur.Selone.Tests.Tests
{
    public class WebElementVisibilityExtensionsTests : TestBase
    {
        [Test]
        public void TestDisplayed()
        {
            var webDriver = Acquire(Browser.Chrome);
            webDriver.OpenTestHtml("WebElementVisibilityExtensions");
            var page = new Page(webDriver);

            Assert.That(page.Target.Displayed().Get(), Is.False);

            page.SetDisplayedButton.Click();
            Assert.That(page.Target.Displayed().Get(), Is.True);

            page.SetVisibleButton.Click();
            //todo В прошлых версиях WebDriver было false, теперь же true
            //todo нужно решить какое поведение должно быть у Displayed & Visible
            Assert.That(page.Target.Displayed().Get(), Is.True);
            //Assert.That(page.Target.Displayed().Get(), Is.False);

            page.SetHiddenButton.Click();
            Assert.That(page.Target.Displayed().Get(), Is.False);

            page.SetAbsentButton.Click();
            Assert.That(page.Target.Displayed().Get(), Is.False);
        }

        [Test]
        public void TestPresent()
        {
            var webDriver = Acquire(Browser.Chrome);
            webDriver.OpenTestHtml("WebElementVisibilityExtensions");
            var page = new Page(webDriver);

            Assert.That(page.Target.Present().Get(), Is.False);

            page.SetDisplayedButton.Click();
            Assert.That(page.Target.Present().Get(), Is.True);

            page.SetVisibleButton.Click();
            Assert.That(page.Target.Present().Get(), Is.True);

            page.SetHiddenButton.Click();
            Assert.That(page.Target.Present().Get(), Is.True);

            page.SetAbsentButton.Click();
            Assert.That(page.Target.Present().Get(), Is.False);
        }

        [Test]
        public void TestVisible()
        {
            var webDriver = Acquire(Browser.Chrome);
            webDriver.OpenTestHtml("WebElementVisibilityExtensions");
            var page = new Page(webDriver);

            Assert.That(page.Target.Visible().Get(), Is.False);

            page.SetDisplayedButton.Click();
            Assert.That(page.Target.Visible().Get(), Is.True);

            page.SetVisibleButton.Click();
            Assert.That(page.Target.Visible().Get(), Is.True);

            page.SetHiddenButton.Click();
            Assert.That(page.Target.Visible().Get(), Is.False);

            page.SetAbsentButton.Click();
            Assert.That(page.Target.Visible().Get(), Is.False);
        }

        private class Page
        {
            public Page(ISearchContext searchContext)
            {
                SetDisplayedButton = searchContext.SearchElement(By.Id("set-displayed"));
                SetVisibleButton = searchContext.SearchElement(By.Id("set-visible"));
                SetHiddenButton = searchContext.SearchElement(By.Id("set-hidden"));
                SetAbsentButton = searchContext.SearchElement(By.Id("set-absent"));
                Target = searchContext.SearchElement(By.Id("placeholder")).SearchElement(By.CssSelector("[data-id='target']"));
            }

            public IWebElement SetDisplayedButton { get; }
            public IWebElement SetVisibleButton { get; }
            public IWebElement SetHiddenButton { get; }
            public IWebElement SetAbsentButton { get; }
            public IWebElement Target { get; }
        }
    }
}