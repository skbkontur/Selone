using OpenQA.Selenium;

namespace Kontur.Selone.Selectors.Context
{
    public class ContextBy : IContextBy
    {
        public ContextBy(ISearchContext searchContext, By by)
        {
            SearchContext = searchContext;
            By = by;
        }

        public ISearchContext SearchContext { get; }
        public By By { get; }
    }
}