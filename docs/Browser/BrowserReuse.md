# Переиспользование браузера между сценариями

Иногда необходимо выполнить несоклько сценариев с использованием браузера. Создание экземпляра `IWebDriver` может быть затратной операцией, которое ведет за собой старт драйвера, который потом запускает браузер.

Для периспользования браузеров между сценариями можно использовать пул. Selone предоставляет реализацию потокобезопасного пула WebDriverPool.

Для создания браузера используется IWebDriverFactory
При возвращенние браузера в пул необходимо подготовить 

Реализации интерфейсов IWebDriverFactory и IWebDriverCleaner
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

public class XxxBrowserCleaner : IWebDriverCleaner
{
    public void Clear(IWebDriver webDriver)
    {
        ...
        webdriver.CloseRedundantWindows();
        ...
    }
}
```
Инициализация пула
```csharp
var factory = new XxxBrowserFactory();
var cleaner = new XxxBrowserCleaner();

var pool = new WebDriverPool(factory, cleaner);
```
Получение браузера из пула
```csharp
var webDriver = pool.Acquire();
...
pool.Release(webDriver);
```
```csharp
using(var wrapper = pool.AcquireWrapper())
{
    var webDriver = wrapper.WrappedDriver;
    ...
}
```
Очистка пула закрывает все браузеры, которые есть в пуле, кроме тех которые используются

```csharp
pool.Clear();
```
## 



## 
Тут описать
- про WebDriverFactory
- про WebDriverPool ??? или это в дополнительной статье с подробностями
