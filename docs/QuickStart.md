# Быстрый старт
Эта пошаговая инструкция позволит создать поддерживаемое, расширяемое решение для автоматизации. Подробное описание, причины и обоснования предлагаемых решений находятся по ссылкам в конце каждого шага.

## 1. Инициализация браузера
Опишем фабрику, которая создаст сконфигурированный экземпляр `IWebDriver`. Фабрика реализует интерфейс `IWebDriverFactory` и его единственный метод `Create`. 

```csharp
public class XxxBrowserFactory : IWebDriverFactory
{
    public IWebDriver Create()
    {
        ...
        var webDriver = new RemoteWebDriver(...);
        ...
        return webDriver;
    }
}
```
Детали:
 - [Где взять браузер](Browser/BrowserSource.md)
 - [Про IWebDriverFactory](Browser/BrowserReuse.md)

## 2. Базовый контрол
todo Возможно этот базовый класс может жить в Selone, возможно он так же пригодится в меанизме "авто-инициализации контролов" через рефлекшен

Удобно выделить базовый класс для всех контролов, в котором будут:
- ссылка на корневой тэг контролов `IWebElement`
- свойства, котрые есть у всех контролов - это свойства присутсвия и видимости

```csharp
public class ControlBase
{
    protected readonly IWebElement container { get; private set; }

    protected ControlBase(ISearchContext context, By selector)
    {
        this.container = context.SearchElement(selector);
    }

    public IProp<bool> IsPresent => container.IsPresent();

    public IProp<bool> IsVisible => container.IsVisible();

    public IProp<bool> IsDisplayed => container.IsDisplayed();
}
```
Детали:
 - [Про объектную модель](Control/ObjectModel.md)
 - [WIP Про SearchElement](Element/Search.md)
 - [Про Селекторы](Selector/Selectors.md)
 - [WIP Про IProp<T>](todo)
 - [WIP Про IsPresent() IsVisible()](todo)

## 3. Примитивные контролы
Опишем примитивные контролы, из которых строится веб-приложение, например, поле ввода, чекбокс, кнопка, текст.

Здесь рассмотрим только пример с полем ввода 
```html
<span id="" class="input-styles">
    <input value="xxx"/>
</span>
```
```csharp
public class Input : ControlBase
{
    private readonly IWebElement input;

    public ControlBase(ISearchContext context, By selector)
        :base(context, selector)
    {
        this.input = Container.SearchElement(By.TagName("input"));
    }

    public IProp<string> Value => input.Value();

    public void SetValue(string value)
    {
        input.Click();
        input.SendKeys(Keys.Control + "a");
        input.SendKeys(Keys.Delete);
        input.SendKeys(value);
        input.SendKeys(Keys.Tab);
    }
}
```
Детали:
 - [Про объектную модель](Control/ObjectModel.md)
 - [Больше примеров примитивных контролов](Control/Primitive.md)

## 4. Составные контролы
На страницах веб-приложения примитивные контролы обычно объединены в формы ввода данных, или группы из нескольких текстовых блоков - такое объединение тоже будем рассматривать как контрол.

```csharp
public class UserForm : ControlBase
{
    protected UserForm(ISearchContext context, By selector)
    {
        LoginInput = new Input(container, By.Id("login"));
        PasswordInput = new Input(container, By.Id("password"));
        RemeberCheckbox = new Checkbox(container, By.Id("remember"));
        ErrorLabel = new Label(container, By.Id("error"));
        SubmitButton = new Button(container, By.Id("submit"));
    }

    public Input LoginInput { get; }
    public Input PasswordInput { get; }
    public Checkbox RemeberCheckbox { get; }
    public Label ErrorLabel { get; }
    public Button SubmitButton { get; }
}
```

todo несколько контролов могут визуально выглядить как группа, и с ними было бы удобнее работать как с одинм составным контролом, но если у них нет общего родительского тега, то использовать ControlBase будет не удобно. тут надо подумать про ControlGroup который бы не имел своего собственного селектора и как следствие тега, но объединял бы в себе несколько контролов.

Детали:
 - [Еще раз про объектную модель](Control/ObjectModel.md)

## 5. Описание страниц
Опишем страницы

```csharp
public class UserSettingsPage : IPage
{
    public UserSettingsPage(IWebDriver webDriver)
    {
        WrappedWebDriver = webDriver
        UserForm = new UserForm(webDriver, By.Id("user-form"));
    }

    public IWebDriver WrappedWebDriver { get; }
    public UserForm UserForm { get; }
}
```
Детали:
 - [Зачем нужны страницы: еще раз про объектную модель](Control/ObjectModel.md)
 - [WIP Что такое страницы](Navigation/Page.md)

## 6. Описать навигацию
Для навигации по веб-проиложению можно использовать прямой переход по URL с помощью метода `GoToUrl` из нативного API.

```csharp
webDriver.Navigate().GoToUrl($"https://site.com/user/settings?userid={userId}");
var page = new UserSettingsPage(webDriver);
```

Такой подход не удобен, когда требуется переиспользование. Лучше сразу позаботиться о специфичных методах, которые будут инкапсулировать знание об URL и параметрах, осуществлять переход и возвращать экземпляр нужной страницы.

```csharp
var page = webDriver.Pages().GoToUserSettings(userId);
```

Если в веб-приложении много страниц, тогда скорее всего присутствует некоторая иерархия, которая заметна и на UI. В таком случае лучше рассмотреть более мастабируемое решение. Пример синтаксиса ниже.

```csharp
var page = webDriver.Pages().User.Settings.GoTo(userId);
```

Детали:
 - [Про навигацию](Navigation/Navigation.md)


## 7. Подключить ассерты

Тут написать про RetryableAssertions с примером с NUnit
В отдельной статье подробнее описать что к чему, примеры с FluentAssertions
примеры работы с ValueTuple и с массивами

## 8. Написать сценарий

Тут пример простенького сценария:

```csharp
//todo кажется лучше не использовать заезжанный пример формы логина
//можно сделать что-нибуть типа строкового конвертера: поле ввода, чекбокс, кнопка convert и результат 

public void Scenario()
{
    var factory = new WebDriverFactory();
    using(var webDriver = factory.Create())
    {
        webDriver.Navigate....
        var page = new Page(webDriver);
        var form = page.LoginForm;
        form.IsPresent.Wait().That(Is.EqualTo(true));
        form.LoginInput.SetValue(...)
        ....
        form.Submit();

    }
}
