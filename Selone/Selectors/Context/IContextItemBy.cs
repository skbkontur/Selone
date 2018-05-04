using OpenQA.Selenium;

namespace Kontur.Selone.Selectors.Context
{
    public interface IContextItemBy
    {
        ISearchContext SearchContext { get; }
        ItemBy ItemBy { get; }
    }
}