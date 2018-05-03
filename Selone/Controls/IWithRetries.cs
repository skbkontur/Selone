using System;
using OpenQA.Selenium;

namespace Kontur.Selone.Controls
{
    public interface IWithRetries
    {
        void Execute(Action<IWebElement> action);
        T Execute<T>(Func<IWebElement, T> func);
    }
}