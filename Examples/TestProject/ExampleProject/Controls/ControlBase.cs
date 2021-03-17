using Kontur.Selone.Extensions;
using Kontur.Selone.Properties;
using OpenQA.Selenium;

namespace Solutions.Controls
{
    public class ControlBase
    {
        protected IWebElement Container;

        protected ControlBase(ISearchContext searchContext, By by)
        {
            Container = searchContext.SearchElement(by);
        }

        public IProp<bool> Present => Container.Present();
        public IProp<bool> Visible => Container.Visible();
    }
}