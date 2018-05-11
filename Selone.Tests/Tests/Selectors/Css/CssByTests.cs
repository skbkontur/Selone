using Kontur.Selone.Selectors.Css;
using NUnit.Framework;

namespace Kontur.Selone.Tests.Tests.Selectors.Css
{
    public class CssByTests
    {
        [Test]
        public void TestThenDescendant()
        {
            var actual = new CssBy("aaa").ThenDescendant("xxx");
            Assert.That(actual.Selector, Is.EqualTo("aaa xxx"));
        }

        [Test]
        public void TestThenChild()
        {
            var actual = new CssBy("aaa").ThenChild("xxx");
            Assert.That(actual.Selector, Is.EqualTo("aaa>xxx"));
        }

        [Test]
        public void TestWithAttributeValue()
        {
            var actual = new CssBy().WithAttribute("xxx", "yyy");
            Assert.That(actual.Selector, Is.EqualTo("[xxx='yyy']"));
        }

        [Test]
        public void TestWithAttribute()
        {
            var actual = new CssBy().WithAttribute("xxx");
            Assert.That(actual.Selector, Is.EqualTo("[xxx]"));
        }

        [Test]
        public void TestWithId()
        {
            var actual = new CssBy().WithId("xxx");
            Assert.That(actual.Selector, Is.EqualTo("#xxx"));
        }

        [Test]
        public void TestWithIndex()
        {
            var actual = new CssBy().WithIndex(123);
            Assert.That(actual.Selector, Is.EqualTo(":nth-child(123)"));
        }

        [Test]
        public void TestXPath()
        {
            var actual = new CssBy("aaa").Css("xxx").Css("yyy");
            Assert.That(actual.Selector, Is.EqualTo("aaaxxxyyy"));
        }
    }
}