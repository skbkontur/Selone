using Kontur.Selone.Tests.Browsers.Factories;
using Kontur.Selone.WebDrivers;

namespace Kontur.Selone.Tests.Browsers
{
    public static class BrowserPool
    {
        public static readonly WebDriverPools<Browser> Instance =
            new WebDriverPools<Browser>()
                .Register(Browser.Chrome, new ChromeDriverFactory(new ChromeDriverFactoryConfiguration
                {
                    ChromeDirectoryPath = @"C:\browsers\Chrome",
                    DriverDirectoryPath = @"C:\browsers\Chrome",
                    WindowSize = new WindowSize {Width = 800, Height = 600}
                }))
                .Register(Browser.Ie, new InternetExplorerDriverFactory(new InternetExplorerDriverFactoryConfiguration
                {
                    DriverDirectoryPath = @"C:\browsers\IE"
                }));
    }
}