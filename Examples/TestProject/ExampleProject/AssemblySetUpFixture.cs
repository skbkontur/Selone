using Kontur.Selone.Extensions;
using Kontur.Selone.WebDrivers;
using NUnit.Framework;
using Solutions.Magic;

[assembly: Parallelizable(ParallelScope.Children)]
[assembly: LevelOfParallelism(2)]

namespace Solutions
{
    [SetUpFixture]
    public class AssemblySetUpFixture
    {
        public static WebDriverPool WebDriverPool;

        [OneTimeSetUp]
        public void SetUp()
        {
            var factory = new ChromeDriverFactory();
            var cleaner = new DelegateWebDriverCleaner(x => x.ResetWindows());
            var disposer = new WebDriverDisposer(x =>
            {
                x.Close();
                x.Quit();
            });
            WebDriverPool = new WebDriverPool(factory, cleaner, disposer);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            WebDriverPool.Clear();
        }
    }
}