using Kontur.Selone.Extensions;
using Kontur.Selone.Selectors.Css;
using NUnit.Framework;
using OpenQA.Selenium;
using Solutions.Magic;

namespace Solutions.Tests
{
    public class Case3CheckCount
    {
        [Test]
        public void WithFind()
        {
            var webDriver = new ChromeDriverFactory().Create();
            webDriver.Navigate().GoToUrl(Urls.OrderList);

            // поиск всех строк с заказами по селектору "[data-tid='Order']" и ожидание в течение пяти секунд
            Assert.That(() => webDriver.FindElements(By.CssSelector("[data-tid='Order']")).Count, Is.EqualTo(5).After(5000, 100));

            webDriver.Dispose();
        }

        [Test]
        public void WithSearch()
        {
            var webDriver = new ChromeDriverFactory().Create();
            webDriver.Navigate().GoToUrl(Urls.OrderList);

            // поиск всех строк с заказами
            var orders = webDriver.SearchElements(x => x.WithTid("Order").FixedByAttribute("data-key"));
            // ожидание количества
            orders.Count.Wait().That(Is.EqualTo(5));

            // преимущества:
            // 1. возможность описать коллекцию заранее один раз
            // 2. возможность задать допоплнительное правило для повторного поиска элемента списка

            webDriver.Dispose();
        }
    }
}