using System.Collections.Generic;
using Kontur.Selone.Controls;
using Kontur.Selone.Selectors;
using OpenQA.Selenium;

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

        public static IEnumerable<IWebElement> SearchElements(this ISearchContext searchContext, ItemBy itemBy)
        {
            return new WebElementsCollection(searchContext, itemBy);
        }

        public static IEnumerable<IWebElement> SearchElements(this ISearchContext searchContext, ItemByLambda itemByLambda)
        {
            return new WebElementsCollection(searchContext, itemByLambda);
        }
    }
}