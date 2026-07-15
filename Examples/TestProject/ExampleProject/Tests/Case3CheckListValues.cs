using System.Linq;
using NUnit.Framework;
using Solutions.Application;
using Solutions.Magic;

namespace Solutions.Tests
{
    public class Case3CheckListValues : TestBase
    {
        [Test]
        public void WithSearch()
        {
            var page = Navigation.GoToPage<OrderListPage>(GetWebDriver(), Urls.OrderList);

            // проверка номеров заказов
            page.OrderTable.Items.Select(x => x.Id.Text).Wait().EqualTo(new[]
            {
                "xxx01", "xxx02", "xxx03", "xxx04", "xxx05"
            });
        }
    }
}
