using Kontur.Selone.Extensions;
using Kontur.Selone.Pages;
using Kontur.Selone.Waiting;
using OpenQA.Selenium;
using Solutions.Controls;
using Solutions.Magic;

namespace Solutions.Application
{
    public class NotificationSettingsPage : IPage, ILoadable
    {
        public NotificationSettingsPage(IWebDriver webDriver)
        {
            WrappedDriver = webDriver;
            Checkbox = webDriver.Search(x => x.WithTid("Checkbox")).Checkbox();
            Input = webDriver.Search(x => x.WithTid("Input")).Input();
        }

        public Checkbox Checkbox { get; }
        public Input Input { get; }

        public IWebDriver WrappedDriver { get; }

        public void WaitLoaded(int? timeout = null)
        {
            Checkbox.Present.Wait().EqualTo(true, timeout);
        }
    }
}
