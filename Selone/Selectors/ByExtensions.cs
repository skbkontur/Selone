using System;
using OpenQA.Selenium;

namespace Kontur.Selone.Selectors
{
    public static class ByExtensions
    {
        public static RootXPathBy Child(this ByDummy dummy)
        {
            return XPathBy.Child();
        }

        public static RootXPathBy Descendant(this ByDummy dummy)
        {
            return XPathBy.Descendant();
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