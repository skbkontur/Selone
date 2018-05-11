using Kontur.Selone.Selectors.XPath;
using NUnit.Framework;

namespace Kontur.Selone.Tests.Tests.Selectors.XPath
{
    public class XPathByTests
    {
        [Test]
        public void TestThenDescendant()
        {
            var actual = new XPathBy().ThenDescendant("*@xxx");
            Assert.That(actual.Selector, Is.EqualTo("//*@xxx"));
        }

        [Test]
        public void TestThenChild()
        {
            var actual = new XPathBy().ThenChild("*@xxx");
            Assert.That(actual.Selector, Is.EqualTo("/*@xxx"));
        }

        [Test]
        public void TestWithAttributeValue()
        {
            var actual = new XPathBy().WithAttribute("xxx", "yyy");
            Assert.That(actual.Selector, Is.EqualTo("[@xxx='yyy']"));
        }

        [Test]
        public void TestWithAttribute()
        {
            var actual = new XPathBy().WithAttribute("xxx");
            Assert.That(actual.Selector, Is.EqualTo("[@xxx]"));
        }

        [Test]
        public void TestWithId()
        {
            var actual = new XPathBy().WithId("xxx");
            Assert.That(actual.Selector, Is.EqualTo("[@id='xxx']"));
        }

        [Test]
        public void TestWithIndex()
        {
            var actual = new XPathBy().WithIndex(123);
            Assert.That(actual.Selector, Is.EqualTo("[124]"));
        }

        [Test]
        public void TestXPath()
        {
            var actual = new XPathBy("aaa").XPath("xxx").XPath("yyy");
            Assert.That(actual.Selector, Is.EqualTo("aaaxxxyyy"));
        }
    }
}