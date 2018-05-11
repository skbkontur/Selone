using System;
using OpenQA.Selenium;

namespace Kontur.Selone.Selectors.XPath
{
    public static class XPathByExtensions
    {
        public static XPathBy XPath(this ByDummy dummy, string xpath = null)
        {
            return new XPathBy(xpath);
        }

        public static XPathBy XChild(this ByDummy dummy, string xpath = null)
        {
            return dummy.XPath().ThenChild(xpath);
        }

        public static XPathBy XDescendant(this ByDummy dummy, string xpath = null)
        {
            return dummy.XPath().ThenDescendant(xpath);
        }

        public static ItemBy FixedBy(this XPathBy xPathBy, Func<XPathBy, IWebElement, int, By> fix)
        {
            return new ItemBy(xPathBy, (e, i) => fix(xPathBy, e, i));
        }

        public static ItemBy FixedByAttribute(this XPathBy xPathBy, string name)
        {
            return xPathBy.WithAttribute(name).FixedBy((xpath, e, i) => xpath.WithAttribute(name, e.GetAttribute(name)));
        }

        public static ItemBy FixedByIndex(this XPathBy xPathBy)
        {
            return xPathBy.FixedBy((xpath, e, i) => xpath.WithIndex(i));
        }
    }
}