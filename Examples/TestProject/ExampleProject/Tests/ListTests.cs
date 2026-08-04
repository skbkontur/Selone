using System.Linq;
using Kontur.Selone.Properties;
using NUnit.Framework;
using Solutions.Application;
using Solutions.Magic;

namespace Solutions.Tests
{
    public class ListTests : TestBase
    {
        [Test]
        public void CountTest()
        {
            var page = Navigation.GoToPage<OrderListPage>(GetWebDriver(), Urls.OrderList);

            // Ожидание количества строк в таблице.
            page.OrderTable.Items.Count.Wait().EqualTo(5);

            // Преимущества:
            // 1. Возможность переиспользовать описание коллекции из page object.
            // 2. Не нужно вручную задавать правило для повторного поиска элемента списка.
        }


        [Test]
        public void CountUpdateTest()
        {
            var page = Navigation.GoToPage<OrderListPage>(GetWebDriver(), Urls.OrderList);

            // Ожидание пяти строк в таблице.
            page.OrderTable.Items.Count.Wait().EqualTo(5);
            // Ожидание трех страниц.
            page.Paging.PagesCount.Wait().EqualTo(3);

            // Поиск Титова.
            page.Filter.SearchTitovButton.Click();

            // Ожидание двух строк в таблице.
            page.OrderTable.Items.Count.Wait().EqualTo(2);
            // Ожидание отсутствия пейджинга.
            page.Paging.Present.Wait().EqualTo(false);
        }

        [Test]
        public void SingleElementTest()
        {
            var page = Navigation.GoToPage<OrderListPage>(GetWebDriver(), Urls.OrderList);

            // Ожидание единственной строки с нужным номером заказа.
            var order = page.OrderTable.Items.Wait().Single(x => x.Id.Text, Is.EqualTo("xxx02"));
            // Клик по строке заказа.
            order.Click();

            // Проверка текста в модальном окне.
            page.Modal.Text.Wait().That(Contains.Substring("xxx02"));

            // Преимущества:
            // 1. Возможность получить элемент, удовлетворяющий проверке, для взаимодействия с ним.
        }

        [Test]
        public void SingleElementUpdateTest()
        {
            var page = Navigation.GoToPage<OrderListPage>(GetWebDriver(), Urls.OrderList);

            // Получение строки с ФИО "Назаров Иван".
            var order = page.OrderTable.Items.Wait().Single(x => x.Fio.Text, Is.EqualTo("Назаров Иван"));

            // Ожидание суммы заказа 150.
            order.Sum.Text.Wait().EqualTo(150);
            // Перезагрузка строки.
            order.ReloadLink.Click();
            // Ожидание суммы заказа 151.
            order.Sum.Text.Wait().EqualTo(151);
        }

        [Test]
        public void ListElementsTest()
        {
            var page = Navigation.GoToPage<OrderListPage>(GetWebDriver(), Urls.OrderList);

            // Проверка номеров заказов.
            page.OrderTable.Items.Select(x => x.Id.Text).Wait().EqualTo(new[]
            {
                "xxx01", "xxx02", "xxx03", "xxx04", "xxx05"
            });
        }

        [Test]
        public void ListElementsTest2()
        {
            var page = Navigation.GoToPage<OrderListPage>(GetWebDriver(), Urls.OrderList);

            // Ожидание появления фильтра.
            page.Filter.Present.Wait().EqualTo(true);

            // Поиск Иванова.
            page.Filter.SearchIvanButton.Click();

            // Ожидаемые значения.
            var expected = new[]
            {
                ("xxx02", false, 150m),
                ("xxx06", false, 160.5m),
                ("xxx11", true, 170.55m)
            };

            // Ожидание значений в строках ("идентификатор заказа", "заказ проверен", "сумма заказа").
            page.OrderTable.Items.Select(x => Props.Create(x.Id.Text, x.Verified.Checked, x.Sum.Text)).Wait()
                .EquivalentTo(expected);
        }

        [Test]
        public void ListElementsUpdateTest()
        {
            var page = Navigation.GoToPage<OrderListPage>(GetWebDriver(), Urls.OrderList);

            // Получение строки с ФИО "Назаров Иван".
            var order = page.OrderTable.Items.Wait().Single(x => x.Fio.Text, Is.EqualTo("Назаров Иван"));
            // Удаление строки.
            order.RemoveLink.Click();

            // Ожидание отсутствия строки.
            order.Present.Wait().EqualTo(false);

            // Ожидание количества строк.
            page.OrderTable.Items.Count.Wait().EqualTo(4);

            // Ожидание идентификаторов заказов.
            page.OrderTable.Items.Select(x => x.Id.Text).Wait().EqualTo(new[] { "xxx01", "xxx03", "xxx04", "xxx05" });
        }
    }
}