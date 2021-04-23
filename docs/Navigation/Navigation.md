# Навигация

- чем навигация по урлам лучше прокликивания ссылок в приложении - когда что использовать
- как делать простые переходы по урлам и почему это становится неудобным - много старниц, большая вложеннность, надо соспоставлять урл с объектной моделью
- сделать специальные методы для перехода по урлам, которые возвращают нужную страницу
- сделать навигацию отражающую структур сервиса


```csharp
public static class NavigationExtensions
{
    public static 
}


public class PageNavigationBase
{
    protected
}

public class UserSettingsPageNavigation
{
    public UserSettingsPage GoTo(Guid userId)
    {

    }
}

public class UserPageNavigation
{
    public UserSettingsPageNavigation Settings => new UserSettingsPageNavigation();
}

public class PagesNavigation
{
    public UserPageNavigation User => new UserPageNavigation();
}

var page = webDriver.Pages().Main.Settings.GoTo(userId);
```