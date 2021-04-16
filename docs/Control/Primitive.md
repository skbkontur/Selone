# Примитивные контролы

Удобно один раз описать примитивные переиспользуемые контролы из которых строится веб-приложение, например, поле ввода, чекбокс, кнопка, текст.

### Input
```html
<span class="input-styles">
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

### Checkbox
```html
<span class="checkbox-styles">
    <input type="checkbox" disabled="false">
</span>
```

```csharp
public class Checkbox : ControlBase
{
    ???
}
```

### Button

```html
<span class="button-styles">
    <button disabled="true">
</span>
```

```csharp
public class Button : ControlBase
{
    ???
}
```

### Label
```html
<span class="text-styles">
    some output
</span>
```

```csharp
public class Label : ControlBase
{
    ???
}
```