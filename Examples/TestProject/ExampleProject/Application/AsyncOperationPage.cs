using Kontur.Selone.Extensions;
using Kontur.Selone.Pages;
using Kontur.Selone.Waiting;
using OpenQA.Selenium;
using Solutions.Controls;
using Solutions.Magic;

namespace Solutions.Application
{
    public class AsyncOperationPage : IPage, ILoadable
    {
        public AsyncOperationPage(IWebDriver webDriver)
        {
            WrappedDriver = webDriver;
            ExecuteButton = webDriver.Search(x => x.WithTid("ExecuteButton")).Button();
            Result = webDriver.Search(x => x.WithTid("Result")).Label();
        }

        public Button ExecuteButton { get; }
        public Label Result { get; }

        public IWebDriver WrappedDriver { get; }

        public void WaitLoaded(int? timeout = null)
        {
            ExecuteButton.Present.Wait().EqualTo(true, timeout);
        }
    }
}
