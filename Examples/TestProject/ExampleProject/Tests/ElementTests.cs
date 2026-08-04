using NUnit.Framework;
using Solutions.Application;
using Solutions.Magic;

namespace Solutions.Tests
{
    public class ElementTests : TestBase
    {
        [Test]
        public void VisibilityTest()
        {
            var page = Navigation.GoToPage<NotificationSettingsPage>(GetWebDriver(), Urls.NotificationSettings);

            // Проверка видимости чекбокса.
            // Ненадежный подход, т.к. чекбокс мог не отрендериться вовремя.
            Assert.That(page.Checkbox.Visible.Get(), Is.True);

            // Проверка отсутствия поля ввода.
            Assert.That(page.Input.Present.Get(), Is.False);

            // Клик по чекбоксу.
            page.Checkbox.Click();

            // Проверка видимости поля ввода.
            Assert.That(page.Input.Visible.Get(), Is.True);

            // Два клика на чекбокс (скрыли, показали поле ввода).
            page.Checkbox.Click();
            page.Checkbox.Click();

            // Проверка видимости поля ввода, повторный поиск элемента не требуется.
            // StaleElementReferenceException не возникнет.
            // Рекомендуемый подход.
            page.Input.Visible.Wait().That(Is.True);

            // Преимущества:
            // 1. Возможность описать поиск элементов один раз.
            // 2. Проверка отсутствия элемента без NoSuchElementException.
            // 3. Нет проблем с StaleElementReferenceException.
        }

        [Test]
        public void TextTest()
        {
            var page = Navigation.GoToPage<AsyncOperationPage>(GetWebDriver(), Urls.AsyncOperation);

            // Проверка видимости кнопки.
            Assert.That(page.ExecuteButton.Visible.Get(), Is.True);
            // Проверка отсутствия результата.
            Assert.That(page.Result.Present.Get(), Is.False);

            // Запуск асинхронной операции.
            page.ExecuteButton.Click();

            // Ожидание результата операции. Возможные подходы:

            // 1. Нестабильный вариант, можно, конечно, добавить Thread.Sleep().
            // Assert.That(result.Text, Is.EqualTo("Успешно выполнено"));

            // 2. Дожидаемся появления результата, потом проверяем текст.
            // Assert.That(result.Visible().Get, Is.True.After(5000, 100));
            // Assert.That(result.Text, Is.EqualTo("Успешно выполнено"));

            // 3. Проверка текста с использованием RetryableAssertions.
            page.Result.Text.Wait().That(Is.EqualTo("Успешно выполнено"));

            // Преимущества:
            // 1. Ожидание значения на еще отсутствующем элементе.
            // 2. Доступно все множество проверок NUnit или другого подключенного фреймворка проверок.
        }
    }
}
