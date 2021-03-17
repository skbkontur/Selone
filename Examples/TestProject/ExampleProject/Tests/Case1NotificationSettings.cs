using Kontur.Selone.Extensions;
using NUnit.Framework;
using OpenQA.Selenium;
using Solutions.Magic;

namespace Solutions.Tests
{
    public class Case1NotificationSettings
    {
        [Test]
        public void WithFind()
        {
            var webDriver = new ChromeDriverFactory().Create();
            webDriver.Navigate().GoToUrl(Urls.NotificationSettings);

            // поиск чекбокса
            var checkbox = webDriver.FindElement(By.CssSelector("[data-tid='Checkbox']"));
            // проверка видимости чекбокса
            Assert.That(checkbox.Displayed, Is.True);

            // проверка отсутствия поля ввода
            Assert.Throws<NoSuchElementException>(() =>
            {
                var dummy = webDriver.FindElement(By.CssSelector("[data-tid='Input']"));
            });

            // клик чекбокс
            checkbox.Click();

            // поиск поля ввода
            var input = webDriver.FindElement(By.CssSelector("[data-tid='Input']"));
            // проверка видимости поля ввода
            Assert.That(input.Displayed, Is.True);

            // два клика на чекбокс (скрыли, показали поле ввода)
            checkbox.Click();
            checkbox.Click();

            // проверка видимости поля ввода - выбросится StaleElementReferenceException
            //Assert.That(input.Displayed, Is.True);

            // повторный поиск поля ввода
            input = webDriver.FindElement(By.CssSelector("[data-tid='Input']"));
            // проверка видимости поля ввода
            Assert.That(input.Displayed, Is.True);

            webDriver.Dispose();
        }

        [Test]
        public void WithSearch()
        {
            var webDriver = new ChromeDriverFactory().Create();
            webDriver.Navigate().GoToUrl(Urls.NotificationSettings);

            // поиск чекбокса и поля ввода
            var checkbox = webDriver.SearchElement(By.CssSelector("[data-tid='Checkbox']"));
            var input = webDriver.SearchElement(By.CssSelector("[data-tid='Input']"));

            // проверка видимости чекбокса
            Assert.That(checkbox.Displayed, Is.True);

            // проверка отсутствия поля ввода
            Assert.That(input.Present().Get(), Is.False);

            // клик по чекбоксу
            checkbox.Click();

            // проверка видимости поля ввода
            Assert.That(input.Displayed, Is.True);

            // два клика на чекбокс (скрыли, показали поле ввода)
            checkbox.Click();
            checkbox.Click();

            // проверка видимости поля ввода, не требуется повторный поиск элемента
            // не будет StaleElementReferenceException
            Assert.That(input.Displayed, Is.True);

            // преимущества:
            // 1. возможность описать поиск элементов один раз
            // 2. проверка отсутствия элемента без NoSuchElementException
            // 3. нет проблем с StaleElementReferenceException

            webDriver.Dispose();
        }
    }
}