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
            WebDriverPool = new WebDriverPool(factory, cleaner);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            WebDriverPool.Clear();
        }
    }
}