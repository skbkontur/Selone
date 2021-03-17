using System.Threading;
using NUnit.Framework;
using Solutions.Application;
using Solutions.Magic;

namespace Solutions.Tests
{
    public class Case3Parallel : TestBase
    {
        [Test]
        public void TestFilter()
        {
            var webDriver = GetWebDriver();
            webDriver.Navigate().GoToUrl(Urls.OrderList);
            var page = new OrderListPage(webDriver);

            Thread.Sleep(3000);
        }

        [Test]
        public void TestDeleteOrder()
        {
            var webDriver = GetWebDriver();
            webDriver.Navigate().GoToUrl(Urls.OrderList);
            var page = new OrderListPage(webDriver);

            Thread.Sleep(3000);
        }

        [Test]
        public void TestEditOrder()
        {
            var webDriver = GetWebDriver();
            webDriver.Navigate().GoToUrl(Urls.OrderList);
            var page = new OrderListPage(webDriver);

            Thread.Sleep(3000);
        }

        [Test]
        public void TestValues()
        {
            var webDriver = GetWebDriver();
            webDriver.Navigate().GoToUrl(Urls.OrderList);
            var page = new OrderListPage(webDriver);

            Thread.Sleep(3000);
        }
    }
}