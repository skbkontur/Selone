# Запуск проекта

## Браузер
Для запуска тестов необходим браузер [Google Chrome](https://www.google.com/intl/ru_ru/chrome/).

Драйвер отдельно скачивать не нужно: начиная с Selenium 4.6 драйвер, соответствующий установленной версии Chrome,
скачивается автоматически (Selenium Manager) при первом запуске тестов.

## .NET
Проект собирается под .NET 8.0.

## Тестовое веб-приложение
Проект работает с собственным тестовым приложением.

Для его запуска необходимо:
* Node.js версии 18+ и любая современная версия npm. Скачать можно [тут](https://nodejs.org/en/).
* Запустить скрипт `_build.cmd` из корневой директории.

После запуска можно открыть [storybook](http://localhost:7890/) или [версию](http://localhost:7890/iframe.html), используемую в тестах.

После этого можно запускать тесты.

# Содержание демо-проекта

Демонстрационный проект построен на связке двух библиотек — Selone и RetryableAssertions. Эти две библиотеки дают мощную базу, которая закрывает любую задачу работы с элементами страниц в функциональных тестах, использующих браузер.

Инфраструктура:

1. Вспомогательные методы отложенных проверок с fluent-интерфейсом — [Extensions.cs](ExampleProject/Magic/Extensions.cs)
2. Пул браузеров для параллельного запуска тестов — [TestBase.cs](ExampleProject/Magic/TestBase.cs)
3. Поиск элементов страницы по атрибуту data-tid — [OrderListPage.cs](ExampleProject/Application/OrderListPage.cs). Поиск по CSS, xPath и подобным селекторам, по селектору с привязкой к атрибуту или индексу реализуются аналогично.
4. Преобразование свойств элементов из строк в более простые типы для более гибких ассертов — [ExampleProject/Property](ExampleProject/Property)
5. Выделенная абстракция для навигации по страницам — [Navigation.cs](ExampleProject/Magic/Navigation.cs)

Примеры описания страниц и элементов страницы с использованием паттерна Page Object:
1. Простой элемент страницы со свойствами — [Checkbox.cs](ExampleProject/Controls/Checkbox.cs)
2. Составной элемент страницы — [OrderRow.cs](ExampleProject/Application/OrderRow.cs)
3. Страница — [OrderListPage.cs](ExampleProject/Application/OrderListPage.cs)

Работа с элементами страницы:
1. Проверка свойств элементов страницы [ElementTests.cs:16](ExampleProject/Tests/ElementTests.cs#L16)
2. Проверка свойств элементов с ожиданием [ElementTests.cs:34](ExampleProject/Tests/ElementTests.cs#L34)
3. Проверка количества элементов списка [ListTests.cs:17](ExampleProject/Tests/ListTests.cs#L17)
4. Получение элементов списка с заданными свойствами [ListTests.cs:50](ExampleProject/Tests/ListTests.cs#L50)
5. Проверка свойств элементов списка [ListTests.cs:83](ExampleProject/Tests/ListTests.cs#L83)