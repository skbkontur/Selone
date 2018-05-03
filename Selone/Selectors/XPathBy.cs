using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;

namespace Kontur.Selone.Selectors
{
    public class XPathBy : By
    {
        private readonly string xpath;

        public XPathBy(string xpath)
        {
            this.xpath = xpath;
        }

        public static RootXPathBy Descendant()
        {
            return new RootXPathBy("//");
        }

        public static RootXPathBy Child()
        {
            return new RootXPathBy("/");
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            return ((IFindsByXPath) context).FindElementByXPath(xpath);
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            return ((IFindsByXPath) context).FindElementsByXPath(xpath);
        }

        public XPathBy WithIndex(int index)
        {
            return Concat($"[{index + 1}]");
        }

        public XPathBy WithAttribute(string name)
        {
            return Concat($"[@{name}]");
        }

        public XPathBy WithAttribute(string name, string value)
        {
            return Concat($"[@{name}='{value}']");
        }

        public XPathBy WithId(string id)
        {
            return WithAttribute("id", id);
        }

        public RootXPathBy ThenDescendant()
        {
            return new RootXPathBy(xpath + "//");
        }

        public RootXPathBy ThenChild()
        {
            return new RootXPathBy(xpath + "/");
        }

        protected XPathBy Concat(string tail)
        {
            return new XPathBy(xpath + tail);
        }
    }
}