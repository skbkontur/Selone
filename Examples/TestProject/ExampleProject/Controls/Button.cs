using Kontur.Selone.Extensions;
using Kontur.Selone.Properties;
using OpenQA.Selenium;
using Solutions.Property;

namespace Solutions.Controls
{
    public class Button : ControlBase
    {
        public Button(ISearchContext searchContext, By by) : base(searchContext, by)
        {
        }

        public IProp<string> Text => Container.Text();
        public IProp<bool> Disabled => Container.Disabled();

        public void Click()
        {
            Container.Click();
        }
    }
}