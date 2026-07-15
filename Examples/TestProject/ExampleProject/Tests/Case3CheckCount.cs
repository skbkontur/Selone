using NUnit.Framework;
using Solutions.Application;
using Solutions.Magic;

namespace Solutions.Tests
{
    public class Case3CheckCount : TestBase
    {
        [Test]
        public void WithSearch()
        {
            var page = Navigation.GoToPage<OrderListPage>(GetWebDriver(), Urls.OrderList);

            // ожидание количества строк в таблице
            page.OrderTable.Items.Count.Wait().EqualTo(5);

            // преимущества:
            // 1. возможность переиспользовать описание коллекции из page object
            // 2. не нужно вручную задавать правило для повторного поиска элемента списка
        }
    }
}
