using System.Linq;
using Kontur.Selone.Extensions;
using Kontur.Selone.Selectors.XPath;
using Kontur.Selone.Tests.Browsers;
using Kontur.Selone.Tests.Extensions;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Kontur.Selone.Tests.Tests
{
    [TestFixture]
    public class WebElementsCollectionTests : TestBase
    {
        [Test]
        public void AbsentElementsHasZeroCount()
        {
            var webDriver = Acquire(Browser.Chrome);
            webDriver.OpenTestHtml("WebElementsCollection");

            var collection = webDriver.SearchElements(x => x.XDescendant().AnyTag().FixedByAttribute("absent"));
            Assert.That(collection, Is.Empty);
        }

        [Test]
        public void FixedByDataId()
        {
            var webDriver = Acquire(Browser.Chrome);
            webDriver.OpenTestHtml("WebElementsCollection");

            var setOriginalButton = webDriver.SearchElement(By.Id("fixed-collection-set-original"));
            var setModifiedButton = webDriver.SearchElement(By.Id("fixed-collection-set-modified"));
            var collection = webDriver.SearchElements(x => x.XDescendant().AnyTag().WithId("fixed-collection").ThenChild().AnyTag().FixedByAttribute("data-id"));

            setOriginalButton.Click();

            var array = collection.ToArray();

            var itemAaa = array[0];
            var itemXxx = array[1];
            var itemYyy = array[2];
            var itemZzz = array[3];

            Assert.That(itemAaa.Text, Is.EqualTo("text-aaa"));
            Assert.That(itemXxx.Text, Is.EqualTo("text-xxx"));
            Assert.That(itemYyy.Text, Is.EqualTo("text-yyy"));
            Assert.That(itemZzz.Text, Is.EqualTo("text-zzz"));

            setModifiedButton.Click();

            Assert.That(itemAaa.Present().Get(), Is.False);
            Assert.That(itemXxx.Text, Is.EqualTo("text-xxx"));
            Assert.That(itemYyy.Text, Is.EqualTo("text-yyy"));
            Assert.That(itemZzz.Text, Is.EqualTo("text-zzz"));
        }

        [Test]
        public void FixedByIndex()
        {
            var webDriver = Acquire(Browser.Chrome);
            webDriver.OpenTestHtml("WebElementsCollection");

            var setOriginalButton = webDriver.SearchElement(By.Id("fixed-collection-set-original"));
            var setModifiedButton = webDriver.SearchElement(By.Id("fixed-collection-set-modified"));
            var collection = webDriver.SearchElements(x => x.XDescendant().AnyTag().WithId("fixed-collection").ThenChild().AnyTag().FixedByIndex());

            setOriginalButton.Click();

            var array = collection.ToArray();

            var item0 = array[0];
            var item1 = array[1];
            var item2 = array[2];
            var item3 = array[3];

            Assert.That(item0.Text, Is.EqualTo("text-aaa"));
            Assert.That(item1.Text, Is.EqualTo("text-xxx"));
            Assert.That(item2.Text, Is.EqualTo("text-yyy"));
            Assert.That(item3.Text, Is.EqualTo("text-zzz"));

            setModifiedButton.Click();

            Assert.That(item0.Text, Is.EqualTo("text-zzz"));
            Assert.That(item1.Text, Is.EqualTo("text-xxx"));
            Assert.That(item2.Text, Is.EqualTo("text-yyy"));
            Assert.That(item3.Present().Get(), Is.False);
        }

        [Test]
        public void NestedAbsentElementsHasZeroCount()
        {
            var webDriver = Acquire(Browser.Chrome);
            webDriver.OpenTestHtml("WebElementsCollection");

            var collection = webDriver.SearchElement(By.Id("absent")).SearchElements(x => x.XDescendant().AnyTag().FixedByAttribute("absent"));
            Assert.That(collection, Is.Empty);
        }

        [Test]
        public void SearchForExistingElements()
        {
            var webDriver = Acquire(Browser.Chrome);
            webDriver.OpenTestHtml("WebElementsCollection");

            var collection = webDriver.SearchElements(x => x.XDescendant().AnyTag().WithId("static-collection").ThenChild().AnyTag().FixedByIndex());
            Assert.That(collection.Select(x => x.Text), Is.EqualTo(new[] {"s1", "s2", "s3", "s4"}));
        }

        [Test]
        public void SearchForLazyElements()
        {
            var webDriver = Acquire(Browser.Chrome);
            webDriver.OpenTestHtml("WebElementsCollection");

            var createCollection = webDriver.SearchElement(By.Id("lazy-collection-create"));
            var removeCollection = webDriver.SearchElement(By.Id("lazy-collection-remove"));
            var lazyCollection = webDriver.SearchElements(x => x.XDescendant().AnyTag().WithId("lazy-collection").ThenChild().AnyTag().FixedByIndex());

            Assert.That(lazyCollection, Is.Empty);
            createCollection.Click();
            Assert.That(lazyCollection.Select(x => x.Text).ToArray(), Is.EqualTo(new[] {"l1", "l2", "l3", "l4"}));
            removeCollection.Click();
            Assert.That(lazyCollection, Is.Empty);
        }
    }
}