using Kontur.Selone.Extensions;
using OpenQA.Selenium;

namespace Kontur.Selone.Tests.Helpers
{
    public static class CustomJavaScriptExecutor
    {
        public static string JsHelperName { get; set; } = "FunctionalTests";

        public static void AssertClientErrorsAbsent(this IWebDriver webDriver)
        {
            var errors = webDriver.InvokeMethod("getErrors");
            if (!string.IsNullOrWhiteSpace(errors))
            {
                throw new ClientErrorsPresentException(errors);
            }
        }

        public static string GetClientDebugLogs(this IWebDriver webDriver)
        {
            return webDriver.InvokeMethod("getDebugLogs");
        }

        private static string InvokeMethod(this IWebDriver webDriver, string functionName)
        {
            var script = $"var result = window.{JsHelperName} && {JsHelperName}.{functionName} && {JsHelperName}.{functionName}(); return result == null ? '' : result;";
            return webDriver.JavaScriptExecutor().ExecuteWithSingleResult<string>(script);
        }
    }
}