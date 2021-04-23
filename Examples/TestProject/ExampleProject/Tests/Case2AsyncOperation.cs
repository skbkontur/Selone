using Kontur.Selone.Extensions;
using NUnit.Framework;
using Solutions.Magic;

namespace Solutions.Tests
{
    public class Case2AsyncOperation
    {
        [Test]
        public void WithSearch()
        {
            var webDriver = new ChromeDriverFactory().Create();
            webDriver.Navigate().GoToUrl(Urls.AsyncOperation);

            // поиск кнопки запуска и тэга с результатом
            var executeButton = webDriver.SearchElement(x => x.WithTid("ExecuteButton"));
            var result = webDriver.SearchElement(x => x.WithTid("Result"));

            // проверка видимости кнопки
            Assert.That(executeButton.Displayed, Is.True);
            // проверка отсутствия результата
            Assert.That(result.Present().Get, Is.False);

            // запуск асинхронной операции
            executeButton.Click();

            // ожидание результата операции:

            // 1. нестабильный вариант, можно конечно добавить Thread.Sleep()
            //Assert.That(result.Text, Is.EqualTo("Успешно выполнено"));

            // 2. дожидаемся появления результата, потом проверяем текст
            // Assert.That(result.Visible().Get, Is.True.After(5000, 100));
            // Assert.That(result.Text, Is.EqualTo("Успешно выполнено"));

            // 3. проверка текста с использованием RetryableAssertions
            result.Text().Wait().That(Is.EqualTo("Успешно выполнено"));

            // преимущества:
            // 1. ожидание значения на еще отсутствующем элементе
            // 2. доступно все множество проверок NUnit или другого подключенного фреймворка проверок

            webDriver.Dispose();
        }
    }
}