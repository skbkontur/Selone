using Kontur.Selone.Extensions;
using Kontur.Selone.Tests.Browsers;
using Kontur.Selone.Tests.Extensions;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Kontur.Selone.Tests.Tests
{
    [TestFixture]
    public class WebElementWrapperTests : TestBase
    {
        [Test]
        public void DisplayedOnAbsentElementThrows()
        {
            var webDriver = Acquire(Browser.Chrome);
            webDriver.OpenTestHtml("WebElementWrapper");

            var absent = webDriver.SearchElement(By.Id("absent"));
            Assert.Throws<NoSuchElementException>(() =>
            {
                var dummy = absent.Displayed;
            });
        }

        [Test]
        public void DisplayedOnAbsentElementThrowsFdsgsd()
        {
            var webDriver = Acquire(Browser.Chrome);
            webDriver.OpenTestHtml("WebElementWrapper");
            webDriver.FindElement(By.CssSelector("body"));

            var absent = webDriver
                         .SearchElement(By.CssSelector("html"))
                         .SearchElement(By.CssSelector("body"))
                         .SearchElement(By.CssSelector("[[absent"))
                         .SearchElement(By.Id("xxx"))
                         .SearchElement(By.ClassName("yyy"));
            //absent.Visible().Wait().That(Is.EqualTo(true));
            absent.Text.Wait().That(Is.EqualTo("xxx"), 1000);
        }

        [Test]
        public void SearchForAbsentElement_DoesNotThrow()
        {
            var webDriver = Acquire(Browser.Chrome);
            webDriver.OpenTestHtml("WebElementWrapper");

            Assert.DoesNotThrow(() =>
            {
                var dummy = webDriver.SearchElement(By.Id("absent"));
            });
        }

        [Test]
        public void SearchForExistingElement()
        {
            var webDriver = Acquire(Browser.Chrome);
            webDriver.OpenTestHtml("WebElementWrapper");

            var staticElement = webDriver.SearchElement(By.Id("static-element"));

            Assert.That(staticElement.Text, Is.EqualTo("I am static"));
        }

        [Test]
        public void SearchForLazyElement()
        {
            var webDriver = Acquire(Browser.Chrome);
            webDriver.OpenTestHtml("WebElementWrapper");

            var createLazyElementButton = webDriver.SearchElement(By.Id("lazy-element-create"));
            var placeholder = webDriver.SearchElement(By.Id("lazy-element-placeholder"));
            var lazyElement = placeholder.SearchElement(By.Id("lazy-element"));

            Assert.Throws<NoSuchElementException>(() =>
            {
                var dummy = lazyElement.Text;
            });

            createLazyElementButton.Click();
            Assert.That(lazyElement.Text, Is.EqualTo("I am lazy"));
        }

        [Test]
        public void SearchForNestedAbsentElement_DoesNotThrow()
        {
            var webDriver = Acquire(Browser.Chrome);
            webDriver.OpenTestHtml("WebElementWrapper");

            Assert.DoesNotThrow(() =>
            {
                var dummy = webDriver.SearchElement(By.Id("absent")).SearchElement(By.Id("nested"));
            });
        }

        [Test]
        public void SearchForNestedLazyElement()
        {
            var webDriver = Acquire(Browser.Chrome);
            webDriver.OpenTestHtml("WebElementWrapper");

            var createLazyElementButton = webDriver.SearchElement(By.Id("lazy-nested-element-create"));
            var placeholder = webDriver.SearchElement(By.Id("lazy-nested-element-placeholder"));
            var lazyElementWrapper = placeholder.SearchElement(By.Id("lazy-nested-element-wrapper"));
            var lazyElement = lazyElementWrapper.SearchElement(By.Id("lazy-nested-element"));

            Assert.Throws<NoSuchElementException>(() =>
            {
                var dummy = lazyElementWrapper.Text;
            });

            createLazyElementButton.Click();
            Assert.That(lazyElement.Text, Is.EqualTo("I am nested lazy"));
        }
    }
}