using Kontur.Selone.Selectors;
using OpenQA.Selenium;

namespace Kontur.Selone.Controls
{
    public class WebElemetsCollection : ItemsCollection<IWebElement>
    {
        public WebElemetsCollection(ISearchContext searchContext, ItemByLambda itemByLambda)
            : base(searchContext, itemByLambda, CreateWebElementWrapper)
        {
        }

        public WebElemetsCollection(ISearchContext searchContext, ItemBy itemBy)
            : base(searchContext, itemBy, CreateWebElementWrapper)
        {
        }

        private static IWebElement CreateWebElementWrapper(ISearchContext searchContext, By by, IWebElement element)
        {
            return new WebElementWrapper(searchContext, by, element);
        }
    }
}