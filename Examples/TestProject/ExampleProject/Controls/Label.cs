using Kontur.Selone.Extensions;
using Kontur.Selone.Properties;
using OpenQA.Selenium;

namespace Solutions.Controls
{
    public class Label : ControlBase
    {
        public Label(ISearchContext searchContext, By by) : base(searchContext, by)
        {
        }

        public IProp<string> Text => Container.Text();
    }
}