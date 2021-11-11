using System;
using OpenQA.Selenium;

namespace Kontur.Selone.WebDrivers
{
    public interface IPooledWebDriver : IWrapsDriver, IDisposable
    {
    }
}