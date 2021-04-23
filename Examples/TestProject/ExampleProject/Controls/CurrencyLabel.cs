using Kontur.Selone.Extensions;
using Kontur.Selone.Properties;
using OpenQA.Selenium;
using Solutions.Property;

namespace Solutions.Controls
{
    public class CurrencyLabel : ControlBase
    {
        public CurrencyLabel(ISearchContext searchContext, By by) : base(searchContext, by)
        {
        }

        public IProp<decimal> Text => Container.Text().Currency();
    }
}