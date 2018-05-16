using System;
using OpenQA.Selenium.Internal;

namespace Kontur.Selone.WebDrivers
{
    public interface IPooledWebDriver : IWrapsDriver, IDisposable
    {
    }
}