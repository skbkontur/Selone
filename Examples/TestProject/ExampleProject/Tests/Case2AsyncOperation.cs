using NUnit.Framework;
using Solutions.Application;
using Solutions.Magic;

namespace Solutions.Tests
{
    public class Case2AsyncOperation : TestBase
    {
        [Test]
        public void WithSearch()
        {
            var page = Navigation.GoToPage<AsyncOperationPage>(GetWebDriver(), Urls.AsyncOperation);

            // проверка видимости кнопки
            Assert.That(page.ExecuteButton.Visible.Get(), Is.True);
            // проверка отсутствия результата
            Assert.That(page.Result.Present.Get, Is.False);

            // запуск асинхронной операции
            page.ExecuteButton.Click();

            // ожидание результата операции:

            // 1. нестабильный вариант, можно конечно добавить Thread.Sleep()
            //Assert.That(result.Text, Is.EqualTo("Успешно выполнено"));

            // 2. дожидаемся появления результата, потом проверяем текст
            // Assert.That(result.Visible().Get, Is.True.After(5000, 100));
            // Assert.That(result.Text, Is.EqualTo("Успешно выполнено"));

            // 3. проверка текста с использованием RetryableAssertions
            page.Result.Text.Wait().That(Is.EqualTo("Успешно выполнено"));

            // преимущества:
            // 1. ожидание значения на еще отсутствующем элементе
            // 2. доступно все множество проверок NUnit или другого подключенного фреймворка проверок
        }
    }
}