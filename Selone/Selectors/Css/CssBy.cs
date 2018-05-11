using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;

namespace Kontur.Selone.Selectors.Css
{
    public class CssBy : By
    {
        public CssBy(string selector = null)
        {
            Selector = selector ?? string.Empty;
        }

        public string Selector { get; }

        public override IWebElement FindElement(ISearchContext context)
        {
            return ((IFindsByCssSelector) context).FindElementByCssSelector(Selector);
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            return ((IFindsByCssSelector) context).FindElementsByCssSelector(Selector);
        }

        public CssBy Css(string tail)
        {
            return new CssBy(Selector + tail);
        }

        public CssBy ThenDescendant(string css = null)
        {
            return Css(" ").Css(css);
        }

        public CssBy ThenChild(string css = null)
        {
            return Css(">").Css(css);
        }

        public CssBy Tag(string tag)
        {
            return Css(tag);
        }

        public CssBy AnyTag()
        {
            return Css("*");
        }

        public CssBy WithId(string id)
        {
            return Css($"#{id}");
        }

        public CssBy WithIndex(int index)
        {
            return Css($":nth-child({index})");
        }

        public CssBy WithAttribute(string name, string value)
        {
            return Css($"[{name}='{value}']");
        }

        public CssBy WithAttribute(string name)
        {
            return Css($"[{name}]");
        }

        public CssBy WithClass(string name)
        {
            return Css($".{name}");
        }
    }
}