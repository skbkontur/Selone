using Kontur.Selone.Extensions;
using Kontur.Selone.Pages;
using OpenQA.Selenium;
using Solutions.Controls;
using Solutions.Magic;

namespace Solutions.Application
{
    public class OrderListPage : IPage
    {
        public OrderListPage(IWebDriver webDriver)
        {
            WrappedDriver = webDriver;
            Filter = webDriver.Search(x => x.WithTid("Filter")).Control<Filter>();
            OrderTable = webDriver.Search(x => x.WithTid("Results")).Control<OrderTable>();
            Paging = webDriver.Search(x => x.WithTid("Paging")).Control<Paging>();
        }

        public Filter Filter { get; }
        public OrderTable OrderTable { get; }
        public Paging Paging { get; }

        public IWebDriver WrappedDriver { get; }
    }
}