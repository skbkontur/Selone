using System;
using System.Collections.ObjectModel;
using System.Drawing;
using Kontur.Selone.Extensions;
using Kontur.Selone.Selectors;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions.Internal;
using OpenQA.Selenium.Internal;

namespace Kontur.Selone.Elements
{
    public class WebElementWrapper : IWebElement, IWrapsElement, IWrapsDriver, ILocatable, ITakesScreenshot, IWithRetries
    {
        private readonly ISearchContext searchContext;
        private readonly By by;
        private IWebElement cachedElement;

        public WebElementWrapper(ISearchContext searchContext, ByLambda byLambda, IWebElement webElement)
            : this(searchContext, byLambda(null), webElement)
        {
        }

        public WebElementWrapper(ISearchContext searchContext, ByLambda byLambda)
            : this(searchContext, byLambda(null))
        {
        }

        public WebElementWrapper(ISearchContext searchContext, By by, IWebElement webElement)
            : this(searchContext, by)
        {
            cachedElement = webElement;
        }

        public WebElementWrapper(ISearchContext searchContext, By by)
        {
            this.searchContext = searchContext;
            this.by = by;
        }

        public string TagName => Execute(x => x.TagName);
        public string Text => Execute(x => x.Text);
        public bool Enabled => Execute(x => x.Enabled);
        public bool Selected => Execute(x => x.Selected);
        public Point Location => Execute(x => x.Location);
        public Size Size => Execute(x => x.Size);
        public bool Displayed => Execute(x => x.Displayed);

        public Point LocationOnScreenOnceScrolledIntoView => Execute(x => x.Locatable().LocationOnScreenOnceScrolledIntoView);
        public ICoordinates Coordinates => Execute(x => x.Locatable().Coordinates);

        public IWebDriver WrappedDriver => searchContext.WebDriver();

        public IWebElement WrappedElement => searchContext.FindElement(by);

        public IWebElement FindElement(By by)
        {
            return Execute(x => x.FindElement(by));
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return Execute(x => x.FindElements(by));
        }

        public Screenshot GetScreenshot()
        {
            return Execute(x => x.Screenshoter().GetScreenshot());
        }

        public void Clear()
        {
            Execute(x => x.Clear());
        }

        public void SendKeys(string text)
        {
            Execute(x => x.SendKeys(text));
        }

        public void Submit()
        {
            Execute(x => x.Submit());
        }

        public void Click()
        {
            Execute(x => x.Click());
        }

        public string GetAttribute(string attributeName)
        {
            return Execute(x => x.GetAttribute(attributeName));
        }

        public string GetCssValue(string propertyName)
        {
            return Execute(x => x.GetCssValue(propertyName));
        }

        public string GetProperty(string propertyName)
        {
            return Execute(x => x.GetProperty(propertyName));
        }

        public void Execute(Action<IWebElement> action)
        {
            Execute(x =>
            {
                action(x);
                return true;
            });
        }

        public T Execute<T>(Func<IWebElement, T> func)
        {
            var attempts = 5;
            while (true)
            {
                var hasAttemts = --attempts > 0;
                cachedElement = cachedElement ?? searchContext.FindElement(by);
                try
                {
                    return func(cachedElement);
                }
                catch (InvalidElementStateException) when (hasAttemts)
                {
                    InvalidateCachedElement();
                }
                catch (StaleElementReferenceException) when (hasAttemts)
                {
                    InvalidateCachedElement();
                }
                catch (InvalidOperationException exception) when (hasAttemts && IsElementIsNotClickableAtPointException(exception))
                {
                    ScrollToCachedElement();
                }
                catch (ElementNotVisibleException) when (hasAttemts)
                {
                    ScrollToCachedElement();
                }
            }
        }

        private static bool IsElementIsNotClickableAtPointException(Exception exception)
        {
            return exception.Message.IndexOf("Element is not clickable at point", StringComparison.OrdinalIgnoreCase) != -1;
        }

        private void ScrollToCachedElement()
        {
            cachedElement.ScrollIntoView();
        }

        private void InvalidateCachedElement()
        {
            cachedElement = null;
        }
    }
}