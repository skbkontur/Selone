using OpenQA.Selenium;

namespace Kontur.Selone.WebDrivers;

public interface IWebDriverDisposer
{
    void Dispose(IWebDriver webDriver);
}