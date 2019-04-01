using System;
using Kontur.Selone.Elements;
using Kontur.Selone.Selectors;
using Kontur.Selone.Selectors.Context;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;

namespace Kontur.Selone.Extensions
{
    public static class SearchContextExtensions
    {
        public static IWebElement SearchElement(this ISearchContext searchContext, By by)
        {
            return new WebElementWrapper(searchContext, by);
        }

        public static IWebElement SearchElement(this ISearchContext searchContext, ByLambda byLambda)
        {
            return new WebElementWrapper(searchContext, byLambda);
        }

        public static IElementsCollection<IWebElement> SearchElements(this ISearchContext searchContext, ItemBy itemBy)
        {
            return new WebElementsCollection(searchContext, itemBy);
        }

        public static IElementsCollection<IWebElement> SearchElements(this ISearchContext searchContext, ItemByLambda itemByLambda)
        {
            return new WebElementsCollection(searchContext, itemByLambda);
        }

        public static IContextBy Search(this ISearchContext searchContext, By by)
        {
            return new ContextBy(searchContext, by);
        }

        public static IContextBy Search(this ISearchContext searchContext, ByLambda byLambda)
        {
            return new ContextBy(searchContext, byLambda(null));
        }

        public static IContextItemBy Search(this ISearchContext searchContext, ItemBy itemBy)
        {
            return new ContextItemBy(searchContext, itemBy);
        }

        public static IContextItemBy Search(this ISearchContext searchContext, ItemByLambda itemByLambda)
        {
            return new ContextItemBy(searchContext, itemByLambda(null));
        }

        public static IWebDriver WebDriver(this ISearchContext searchContext)
        {
            return searchContext as IWebDriver ?? (searchContext as IWrapsDriver)?.WrappedDriver ?? throw new Exception("SearchContext is not an IWebDriver and does not implement IWrapsDriver");
        }

        public static ISearchContext Root(this ISearchContext searchContext)
        {
            return searchContext.WebDriver();
        }
    }
}