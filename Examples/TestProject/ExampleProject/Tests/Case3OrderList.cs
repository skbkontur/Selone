using System.Linq;
using Kontur.Selone.Properties;
using NUnit.Framework;
using Solutions.Application;
using Solutions.Magic;

namespace Solutions.Tests
{
    public class Case3OrderList : TestBase
    {
        [Test]
        public void TestFilter()
        {
            var page = Navigation.GoToPage<OrderListPage>(GetWebDriver(), Urls.OrderList);

            // ожидание пяти строк в таблице
            page.OrderTable.Items.Count.Wait().EqualTo(5);
            // ожидание трех страниц
            page.Paging.PagesCount.Wait().EqualTo(3);

            // поиск Титова
            page.Filter.SearchTitovButton.Click();

            // ожидание двух строк в таблице
            page.OrderTable.Items.Count.Wait().EqualTo(2);
            // ожидание отсутствия пейджинга
            page.Paging.Present.Wait().EqualTo(false);
        }

        [Test]
        public void TestDeleteOrder()
        {
            var page = Navigation.GoToPage<OrderListPage>(GetWebDriver(), Urls.OrderList);

            // получение строки с ФИО "Назаров Иван"
            var order = page.OrderTable.Items.Wait().Single(x => x.Fio.Text, Is.EqualTo("Назаров Иван"));
            // удаление строки
            order.RemoveLink.Click();

            // ожидание отсутствия строки
            order.Present.Wait().EqualTo(false);

            // ожидание количества строк
            page.OrderTable.Items.Count.Wait().EqualTo(4);

            // ожидание идентификаторов заказов
            page.OrderTable.Items.Select(x => x.Id.Text).Wait().EqualTo(new[] {"xxx01", "xxx03", "xxx04", "xxx05"});
        }

        [Test]
        public void TestEditOrder()
        {
            var page = Navigation.GoToPage<OrderListPage>(GetWebDriver(), Urls.OrderList);

            // получение строки с ФИО "Назаров Иван"
            var order = page.OrderTable.Items.Wait().Single(x => x.Fio.Text, Is.EqualTo("Назаров Иван"));

            // ожидание суммы заказа 150
            order.Sum.Text.Wait().EqualTo(150);
            // перезагрузка строки
            order.ReloadLink.Click();
            // ожидание суммы заказа 151
            order.Sum.Text.Wait().EqualTo(151);
        }

        [Test]
        public void TestContent()
        {
            var page = Navigation.GoToPage<OrderListPage>(GetWebDriver(), Urls.OrderList);

            // ожидание появления фильтра
            page.Filter.Present.Wait().EqualTo(true);

            // поиск Иванова
            page.Filter.SearchIvanButton.Click();

            // ожидаемые значения
            var expected = new[]
            {
                ("xxx02", false, 150m),
                ("xxx06", false, 160.5m),
                ("xxx11", true, 170.55m)
            };

            // ожидание значений в строках ("идентификатор заказа", "заказа проверен", "сумма заказа")
            page.OrderTable.Items.Select(x => Props.Create(x.Id.Text, x.Verified.Checked, x.Sum.Text)).Wait().EquivalentTo(expected);
        }
    }
}
