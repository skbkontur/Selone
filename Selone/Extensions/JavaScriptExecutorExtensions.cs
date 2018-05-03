using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace Kontur.Selone.Extensions
{
    public static class JavaScriptExecutorExtensions
    {
        public static void ExecuteWithVoidResult(this IJavaScriptExecutor webDriver, string script, params object[] args)
        {
            webDriver.ExecuteScript(script, args);
        }

        public static T ExecuteWithSingleResult<T>(this IJavaScriptExecutor webDriver, string script, params object[] args)
        {
            var result = webDriver.ExecuteScript(script, args);
            return result == null || result is T ? (T) result : ((ICollection<object>) result).Cast<T>().Single();
        }

        public static T[] ExecuteWithCollectionResult<T>(this IJavaScriptExecutor webDriver, string script, params object[] args)
        {
            var result = webDriver.ExecuteScript(script, args);
            return result == null || result is T ? new[] {(T) result} : ((ICollection<object>) result).Cast<T>().ToArray();
        }
    }
}