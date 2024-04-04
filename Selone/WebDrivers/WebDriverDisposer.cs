using System;
using OpenQA.Selenium;

namespace Kontur.Selone.WebDrivers;

public class WebDriverDisposer : IWebDriverDisposer
{
    private readonly Action<IWebDriver> disposeAction;

    public WebDriverDisposer(Action<IWebDriver> disposeAction)
    {
        this.disposeAction = disposeAction;
    }

    public void Dispose(IWebDriver webDriver)
    {
        disposeAction?.Invoke(webDriver);
    }
}