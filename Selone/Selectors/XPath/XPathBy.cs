using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;

namespace Kontur.Selone.Selectors.XPath
{
    public class XPathBy : By
    {
        public XPathBy(string selector = null)
        {
            Selector = selector ?? string.Empty;
        }

        public string Selector { get; }

        public override IWebElement FindElement(ISearchContext context)
        {
            return ((IFindsByXPath) context).FindElementByXPath(Selector);
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            return ((IFindsByXPath) context).FindElementsByXPath(Selector);
        }

        public new XPathBy XPath(string tail)
        {
            return new XPathBy(Selector + tail);
        }

        public XPathBy ThenDescendant(string xpath = null)
        {
            return XPath("//").XPath(xpath);
        }

        public XPathBy ThenChild(string xpath = null)
        {
            return XPath("/").XPath(xpath);
        }

        public XPathBy Tag(string tag)
        {
            return XPath(tag);
        }

        public XPathBy AnyTag()
        {
            return XPath("*");
        }

        public XPathBy WithId(string id)
        {
            return WithAttribute("id", id);
        }

        public XPathBy WithIndex(int index)
        {
            return XPath($"[{index + 1}]");
        }

        public XPathBy WithAttribute(string name)
        {
            return XPath($"[@{name}]");
        }

        public XPathBy WithAttribute(string name, string value)
        {
            return XPath($"[@{name}='{value}']");
        }
    }
}