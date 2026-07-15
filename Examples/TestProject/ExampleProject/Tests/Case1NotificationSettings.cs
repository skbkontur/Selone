using NUnit.Framework;
using Solutions.Application;
using Solutions.Magic;

namespace Solutions.Tests
{
    public class Case1NotificationSettings : TestBase
    {
        [Test]
        public void WithSearch()
        {
            var page = Navigation.GoToPage<NotificationSettingsPage>(GetWebDriver(), Urls.NotificationSettings);

            // проверка видимости чекбокса
            Assert.That(page.Checkbox.Visible.Get(), Is.True);

            // проверка отсутствия поля ввода
            Assert.That(page.Input.Present.Get(), Is.False);

            // клик по чекбоксу
            page.Checkbox.Click();

            // проверка видимости поля ввода
            Assert.That(page.Input.Visible.Get(), Is.True);

            // два клика на чекбокс (скрыли, показали поле ввода)
            page.Checkbox.Click();
            page.Checkbox.Click();

            // проверка видимости поля ввода, не требуется повторный поиск элемента
            // не будет StaleElementReferenceException
            Assert.That(page.Input.Visible.Get(), Is.True);

            // преимущества:
            // 1. возможность описать поиск элементов один раз
            // 2. проверка отсутствия элемента без NoSuchElementException
            // 3. нет проблем с StaleElementReferenceException
        }
    }
}
