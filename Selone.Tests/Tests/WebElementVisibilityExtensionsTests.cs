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

            Assert.False(page.Target.Displayed().Get());

            page.SetDisplayedButton.Click();
            Assert.True(page.Target.Displayed().Get());

            page.SetVisibleButton.Click();
            //todo В прошлых версиях WebDriver было false, теперь же true
            //todo нужно решить какое поведение должно быть у Displayed & Visible
            Assert.True(page.Target.Displayed().Get());
            //Assert.False(page.Target.Displayed().Get());

            page.SetHiddenButton.Click();
            Assert.False(page.Target.Displayed().Get());

            page.SetAbsentButton.Click();
            Assert.False(page.Target.Displayed().Get());
        }

        [Test]
        public void TestPresent()
        {
            var webDriver = Acquire(Browser.Chrome);
            webDriver.OpenTestHtml("WebElementVisibilityExtensions");
            var page = new Page(webDriver);

            Assert.False(page.Target.Present().Get());

            page.SetDisplayedButton.Click();
            Assert.True(page.Target.Present().Get());

            page.SetVisibleButton.Click();
            Assert.True(page.Target.Present().Get());

            page.SetHiddenButton.Click();
            Assert.True(page.Target.Present().Get());

            page.SetAbsentButton.Click();
            Assert.False(page.Target.Present().Get());
        }

        [Test]
        public void TestVisible()
        {
            var webDriver = Acquire(Browser.Chrome);
            webDriver.OpenTestHtml("WebElementVisibilityExtensions");
            var page = new Page(webDriver);

            Assert.False(page.Target.Visible().Get());

            page.SetDisplayedButton.Click();
            Assert.True(page.Target.Visible().Get());

            page.SetVisibleButton.Click();
            Assert.True(page.Target.Visible().Get());

            page.SetHiddenButton.Click();
            Assert.False(page.Target.Visible().Get());

            page.SetAbsentButton.Click();
            Assert.False(page.Target.Visible().Get());
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