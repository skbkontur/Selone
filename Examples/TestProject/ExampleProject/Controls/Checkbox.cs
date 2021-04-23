using Kontur.Selone.Extensions;
using Kontur.Selone.Properties;
using OpenQA.Selenium;
using Solutions.Property;

namespace Solutions.Controls
{
    public class Checkbox : ControlBase
    {
        public Checkbox(ISearchContext searchContext, By by) : base(searchContext, by)
        {
        }

        public IProp<string> Text => Container.Text();
        public IProp<bool> Disabled => Container.Disabled();
        public IProp<bool> Checked => Container.Checked();

        public void Click()
        {
            Container.Click();
        }
    }
}