using System.Linq;
using NUnit.Framework;
using Solutions.Application;
using Solutions.Magic;

namespace Solutions.Tests
{
    public class Case3GetSingleElement : TestBase
    {
        [Test]
        public void WithSearch()
        {
            var page = Navigation.GoToPage<OrderListPage>(GetWebDriver(), Urls.OrderList);

            // ожидание единственной строки с нужным номером заказа
            var order = page.OrderTable.Items.Wait().Single(x => x.Id.Text, Is.EqualTo("xxx02"));
            // клик по строке заказа
            order.Click();

            // проверка текста в модальном окне
            page.Modal.Text.Wait().That(Contains.Substring("xxx02"));

            // преимущества:
            // 1. возможность получить элемент, удовлетворяющий проверке для взаимодействия с ним
        }
    }
}
