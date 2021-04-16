# ComboBox

```csharp
public class ComboBox : ContorlBase
{
	public ComboBox(ISearchContext searchContext, By by) : base(searchContext, by)
	{
		ComboBoxPopup = new ComboBoxPopup(Container, ...);
	}

	public void Click()
	{
		...
	}

	public ComboBoxPopup Popup { get; }
}

public class ComboBoxPopup : ContorlBase
{
	public ComboBoxPopup(ISearchContext searchContext, By by) : base(searchContext, by)
	{
		Items = new ElementsCollection<ComboBoxItem>(Container, x => x.....FixedByIndex(), (sc, selector, we) => new ComboBoxItem(sc, selector));
	}

	public IElementsCollection<ComboBoxItem> Items { get; }
}

public class ComboBoxItem : ContorlBase
{
	public ComboBoxItem(ISearchContext searchContext, By by) : base(searchContext, by)
	{
		...
	}
	
	public IProp<string> Text => ...;

	public void Click()
	{
		...
	}
	
	...
}

public static class ComboBoxExtensions
{
	public static void SelectItem(this ComboBox comboBox, int index)
	{
		comboBox.Click();
		comboBox.Popup.Items.Wait().ElementAt(index).Click();
	}

	public static void SelectItem<T>(this ComboBox comboBox, Func<ComboBoxItem, IProp<T>> selector, IResolseConstraint constraint)
	{
		comboBox.Click();
		comboBox.Popup.Items.Wait().Single(selector, constraint).Click();
	}

	public static void SelectItem(this ComboBox comboBox, int text)
	{
		comboBox.SelectItem(x => x.Text, Is.EqualTo(text));
	}
}
```

