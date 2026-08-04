using Kontur.Selone.Extensions;
using Kontur.Selone.Pages;
using Kontur.Selone.Waiting;
using OpenQA.Selenium;
using Solutions.Controls;
using Solutions.Magic;

namespace Solutions.Application
{
    public class OrderListPage : IPage, ILoadable
    {
        public OrderListPage(IWebDriver webDriver)
        {
            WrappedDriver = webDriver;
            Filter = webDriver.Search(x => x.WithTid("Filter")).Control<Filter>();
            OrderTable = webDriver.Search(x => x.WithTid("Results")).Control<OrderTable>();
            Paging = webDriver.Search(x => x.WithTid("Paging")).Control<Paging>();
            Modal = webDriver.Search(x => x.WithTid("OrderModal")).Modal();
        }

        public Filter Filter { get; }
        public OrderTable OrderTable { get; }
        public Paging Paging { get; }
        public Modal Modal { get; }

        public IWebDriver WrappedDriver { get; }

        public void WaitLoaded(int? timeout = null)
        {
            OrderTable.Present.Wait().EqualTo(true, timeout);
        }
    }
}