# Selone
Selone is a kit with base infrastructure for quick start of your own set of browser tests or web crawler.

Selone is not yet another Selenium.WebDriver wrapper. Selone does not wrap, does not hide or narrow WebDriver functionality and does not prohibit it from being used. 

Selone is not about assertions but it provides abilitily for easy connect of third-party assertion library. For example NUnit with `Assert.That` and `IResolveConstraint` or FluentAssertions with `Should()` method.

Selenium.WebDriver - it is low-level API for web browsers. So its direct usage in your high-level code may be inconvenient.

Selone looks like extension that brings stability and more convenient API for your high-level code

## Features
### Search for absent element
If element doesn't exist then native method `FindElement` will throw `ElementNotFoundException`
```csharp
var element = searchContext.FindElement(By.Id("absent-element-id")); //throws ElementNotFoundException
```
Alternative is to use `SearchElement`. It is the extension method for native `ISearchContext` that returns implementation of native `IWebElement`
```csharp
var element = searchContext.SearchElement(By.Id("absent-element-id"));
//some actions
element.Click();
```
Of course you can search nested elements inside absent elements
```csharp
var container = searchContext.SearchElement(By.Id("absent-container-id"));
var element = container.SearchElement(By.Id("absent-element-id"));
//some actions
element.Click();
```
So, using `SearchElement` method you can pre-declare and initialize all necessary controls in one place

### Automatic search of reattached element
You can't work with reattached element which was found using `FindElement` method. If element was detached from DOM and then attached it becomes another element and you will get `StaleElementReferenceException` interacting with it.
```csharp
var element = searchContext.FindElement(By.Id("disappearing-element"));
searchContext.FindElement(By.Id("remove-from-dom-button")).Click();
searchContext.FindElement(By.Id("insert-into-dom-button")).Click();
element.Click(); //throws StaleElementReferenceException
```
Alternative is to use `SearchElement` method. Element searched by `SearchElement` will handle `StaleElementReferenceException` itself and action with such element would be successfuly done.
```csharp
var element = searchContext.SearchElement(By.Id("disappearing-element"));
searchContext.FindElement(By.Id("remove-from-dom-button")).Click();
searchContext.FindElement(By.Id("insert-into-dom-button")).Click();
element.Click(); //ok
```
### Ability to get element display and presence state
Native `IsDisplayed` property throws exception for absent element and there is no `IsPresent` property.
Selone provides three extension methods for `IWebElement` interface: `Displayed()`, `Visible()`, `Present()`. Of course it's better to use them with element searched by `SearchElement` but you can also use it with element received from `FindElement` method.
```csharp
var element = searchContext.SearchElement(By.Id("target"));
var isDisplayed = element.Displayed().Get();
var isVisible = element.Visible().Get();
var isPresent = element.Present().Get();
```
### Extensible By selector
...
### Simple but smart collection of elements
...
### ...
...
