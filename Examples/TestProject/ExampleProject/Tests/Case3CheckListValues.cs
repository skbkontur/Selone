using System.Linq;
using Kontur.Selone.Extensions;
using Kontur.Selone.Selectors.Css;
using NUnit.Framework;
using OpenQA.Selenium;
using Solutions.Magic;

namespace Solutions.Tests
{
    public class Case3CheckListValues
    {
        [Test]
        public void WithFind()
        {
            var webDriver = new ChromeDriverFactory().Create();
            webDriver.Navigate().GoToUrl(Urls.OrderList);

            // проверка номеров заказов
            Assert.That(() => webDriver.FindElements(By.CssSelector("[data-tid='Order']")).Select(x => x.FindElement(By.CssSelector("[data-tid='Id']")).Text), Is.EqualTo(new[]
            {
                "xxx01", "xxx02", "xxx03", "xxx04", "xxx05"
            }).After(5000, 100));

            webDriver.Dispose();
        }

        [Test]
        public void WithSearch()
        {
            var webDriver = new ChromeDriverFactory().Create();
            webDriver.Navigate().GoToUrl(Urls.OrderList);

            // описание строк с заказами
            var orders = webDriver.SearchElements(x => x.WithTid("Order").FixedByKey());

            // проверка номеров заказов
            orders.Select(x => x.SearchElement(s => s.WithTid("Id")).Text).Wait().That(Is.EqualTo(new[]
            {
                "xxx01", "xxx02", "xxx03", "xxx04", "xxx05"
            }));

            webDriver.Dispose();
        }
    }
}