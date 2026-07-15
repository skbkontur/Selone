using System;
using Kontur.Selone.Pages;
using OpenQA.Selenium;

namespace Solutions.Magic;

public static class Navigation
{
    public static TPage GoToPage<TPage>(IWebDriver webDriver, string url) where TPage : IPage
    {
        var page = (TPage) Activator.CreateInstance(typeof(TPage), webDriver);
        return page.Browse(() => webDriver.Navigate().GoToUrl(url));
    }
}