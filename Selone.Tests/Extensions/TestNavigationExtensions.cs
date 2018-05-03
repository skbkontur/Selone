using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Kontur.Selone.Tests.Extensions
{
    public static class TestNavigationExtensions
    {
        public static void OpenTestHtml(this IWebDriver webDriver, string file)
        {
            webDriver.Navigate().GoToUrl(GetHtmlPath($"{file}.html"));
            var body = webDriver.FindElement(By.TagName("body"));
            Assert.That(body.TagName, Is.EqualTo("body"));
        }

        private static string GetHtmlPath(string file)
        {
            return "file:///" + Path.GetFullPath(Path.Combine(TestContext.CurrentContext.TestDirectory, "..", "..", "..", "Selone.Tests", "Htmls", file));
        }
    }
}