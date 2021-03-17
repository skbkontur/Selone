using Kontur.Selone.Extensions;
using Kontur.Selone.Properties;
using Kontur.Selone.Selectors.Css;
using OpenQA.Selenium;
using Solutions.Property;

namespace Solutions.Controls
{
    public class Input : ControlBase
    {
        private readonly IWebElement input;

        public Input(ISearchContext searchContext, By by) : base(searchContext, by)
        {
            input = Container.SearchElement(x => x.Css("input"));
        }

        public IProp<string> Value => input.Value();
        public IProp<bool> Disabled => Container.Disabled();

        public void SetValue(string value)
        {
            input.SendKeys(Keys.Control + "a");
            input.SendKeys(Keys.Delete);
            input.SendKeys(value);
        }
    }
}