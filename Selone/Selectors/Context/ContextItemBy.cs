using OpenQA.Selenium;

namespace Kontur.Selone.Selectors.Context
{
    public class ContextItemBy : IContextItemBy
    {
        public ContextItemBy(ISearchContext searchContext, ItemBy itemBy)
        {
            SearchContext = searchContext;
            ItemBy = itemBy;
        }

        public ISearchContext SearchContext { get; }
        public ItemBy ItemBy { get; }
    }
}