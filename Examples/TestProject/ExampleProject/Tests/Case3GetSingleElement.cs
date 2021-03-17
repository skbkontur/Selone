using System.Linq;
using Kontur.Selone.Extensions;
using Kontur.Selone.Selectors.Css;
using NUnit.Framework;
using OpenQA.Selenium;
using Solutions.Magic;

namespace Solutions.Tests
{
    public class Case3GetSingleElement
    {
        [Test]
        public void WithFind()
        {
            var webDriver = new ChromeDriverFactory().Create();
            webDriver.Navigate().GoToUrl(Urls.OrderList);

            // ожидание появления строки с нужным номером заказа
            Assert.That(() => webDriver.FindElements(By.CssSelector("[data-tid='Order']"))
                                       .Select(x => x.FindElement(By.CssSelector("[data-tid='Id']")).Text),
                        Does.Contain("xxx02").After(5000, 100)
            );

            // поиск всех строк с заказами
            var orders = webDriver.FindElements(By.CssSelector("[data-tid='Order']"));
            // взятие единственной строки с нужным номером заказа
            var order = orders.Single(x => x.FindElement(By.CssSelector("[data-tid='Id']")).Text == "xxx02");
            // клик по строке заказа
            order.Click();

            // поиск модального окна
            var modal = webDriver.FindElement(By.CssSelector("[data-tid='OrderModal']"));
            // проверка текста в модальном окне
            Assert.That(modal.Text, Contains.Substring("xxx02"));

            webDriver.Dispose();
        }

        [Test]
        public void WithSearch()
        {
            var webDriver = new ChromeDriverFactory().Create();
            webDriver.Navigate().GoToUrl(Urls.OrderList);

            // описание списка заказов и модального окна
            var orders = webDriver.SearchElements(x => x.WithTid("Order").FixedByKey());
            var modal = webDriver.SearchElement(x => x.WithTid("OrderModal"));

            // ожидание единственной строки с нужным номером заказа
            var order = orders.Wait()
                              .Single(
                                  x => x.SearchElement(s => s.WithTid("Id")).Text,
                                  Is.EqualTo("xxx02")
                              );
            // клик по строке заказа
            order.Click();

            // проверка текста в модальном окне
            modal.Text().Wait().That(Contains.Substring("xxx02"));

            // преимущества:
            // 1. возможность получить элемент, удовлетворяющий проверке для взаимодействия с ним

            webDriver.Dispose();
        }
    }
}