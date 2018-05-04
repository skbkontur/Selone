using OpenQA.Selenium;

namespace Kontur.Selone.Selectors.Context
{
    public interface IContextBy
    {
        ISearchContext SearchContext { get; }
        By By { get; }
    }
}