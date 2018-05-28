using System;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using Kontur.Selone.Elements;
using Kontur.Selone.Properties;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Internal;

namespace Kontur.Selone.Extensions
{
    public static class WebElementExtensions
    {
        private static readonly Regex whitespaces = new Regex(@"\s+", RegexOptions.Compiled);

        public static IWebDriver WebDriver(this IWebElement webElement)
        {
            return ((IWrapsDriver) webElement).WrappedDriver;
        }

        public static ITakesScreenshot Screenshoter(this IWebElement webDriver)
        {
            return (ITakesScreenshot) webDriver;
        }

        public static void Execute(this IWebElement webElement, Action<IWebElement> action)
        {
            if (webElement is IWithRetries withRetries)
            {
                withRetries.Execute(action);
            }
            else
            {
                action(webElement);
            }
        }

        public static T Execute<T>(this IWebElement webElement, Func<IWebElement, T> func)
        {
            return webElement is IWithRetries withRetries ? withRetries.Execute(func) : func(webElement);
        }

        public static void Action(this IWebElement webElement, Func<Actions, IWebElement, Actions> action)
        {
            webElement.Execute(x => action(new Actions(x.WebDriver()), x).Perform());
        }

        public static void ExecuteJs(this IWebElement webElement, string js, params object[] args)
        {
            webElement.Execute(x =>
            {
                var jsToExecute = $"(function(x, args){{{js}}})(arguments[0], Array.prototype.slice.call(arguments, 1))";
                var jsArguments = new[] {x}.Concat(args).ToArray();
                var javaScriptExecutor = x.WebDriver().JavaScriptExecutor();
                javaScriptExecutor.ExecuteWithVoidResult(jsToExecute, jsArguments);
            });
        }

        public static void ScrollIntoView(this IWebElement webElement)
        {
            webElement.ExecuteJs("x.scrollIntoView();");
        }

        public static void ClickLeftUp(this IWebElement webElement)
        {
            webElement.Action((a, e) => a.MoveToElement(e, 0, 0).Click());
        }

        public static void ClickRightUp(this IWebElement webElement)
        {
            webElement.Action((a, e) => a.MoveToElement(e, e.Size.Width - 1, 0).Click());
        }

        public static void Mouseover(this IWebElement webElement)
        {
            webElement.Action((a, e) => a.MoveToElement(e));
        }

        public static void DoubleClick(this IWebElement webElement)
        {
            webElement.Action((a, e) => a.DoubleClick(e));
        }

        public static IControlProperty<bool> Present(this IWebElement webElement)
        {
            return webElement.PropertyNoCheck(IsPresent, "IsPreset");
        }

        public static IControlProperty<bool> Visible(this IWebElement webElement)
        {
            return webElement.PropertyNoCheck(IsVisible, "IsVisible");
        }

        public static IControlProperty<bool> Displayed(this IWebElement webElement)
        {
            return webElement.PropertyNoCheck(IsDisplayed, "IsDisplayed");
        }

        public static IControlProperty<bool> Enabled(this IWebElement webElement)
        {
            return webElement.Property(x => x.Enabled, "IsEnabled");
        }

        public static IControlProperty<bool> Selected(this IWebElement webElement)
        {
            return webElement.Property(x => x.Selected, "IsSelected");
        }

        public static IControlProperty<Point> Location(this IWebElement webElement)
        {
            return webElement.Property(x => x.Location, "Location");
        }

        public static IControlProperty<Size> Size(this IWebElement webElement)
        {
            return webElement.Property(x => x.Size, "Size");
        }

        public static IControlProperty<string> Text(this IWebElement webElement)
        {
            return webElement.Property(x => x.Text, "Text");
        }

        public static IControlProperty<string[]> Classes(this IWebElement webElement)
        {
            return webElement.Property(x => ExtractClasses(x.GetAttribute("class")), "Classes");
        }

        public static IControlProperty<string> Value(this IWebElement webElement)
        {
            return webElement.Attribute("value");
        }

        public static IControlProperty<string> TextContent(this IWebElement webElement)
        {
            return webElement.PropertyNoCheck(x => x.GetAttribute("textContent"), "TextContent");
        }

        public static IControlProperty<string> Attribute(this IWebElement webElement, string attributeName)
        {
            return webElement.Property(x => x.GetAttribute(attributeName), $"[{attributeName}]");
        }

        public static IControlProperty<string> AttributeNoCheck(this IWebElement webElement, string attributeName)
        {
            return webElement.PropertyNoCheck(x => x.GetAttribute(attributeName), $"[{attributeName}]");
        }

        public static IControlProperty<T> Property<T>(this IWebElement webElement, Func<IWebElement, T> getValue, string description)
        {
            return ControlProperty.Create(() => webElement.IsVisible() ? getValue(webElement) : throw new ElementNotVisibleException("not visible"), description);
        }

        public static IControlProperty<T> PropertyNoCheck<T>(this IWebElement webElement, Func<IWebElement, T> getValue, string description)
        {
            return ControlProperty.Create(() => getValue(webElement), description);
        }

        private static bool IsPresent(this IWebElement webElement)
        {
            try
            {
                return webElement.Execute(x => !string.IsNullOrEmpty(x.TagName));
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private static bool IsVisible(this IWebElement webElement)
        {
            try
            {
                return webElement.Execute(x => x.Displayed && x.Size.Height > 0 && x.Size.Width > 0 ? true : throw new ElementNotVisibleException());
            }
            catch (NoSuchElementException)
            {
                return false;
            }
            catch (ElementNotVisibleException)
            {
                return false;
            }
        }

        private static bool IsDisplayed(this IWebElement webElement)
        {
            try
            {
                return webElement.Execute(x => x.Displayed);
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private static string[] ExtractClasses(string classes)
        {
            return whitespaces.Split(classes).Where(x => !string.IsNullOrEmpty(x)).ToArray();
        }
    }
}