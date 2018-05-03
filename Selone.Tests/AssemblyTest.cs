using Kontur.Selone.Tests.Browsers;
using NUnit.Framework;

[assembly: Parallelizable(ParallelScope.Children)]
[assembly: LevelOfParallelism(4)]

namespace Kontur.Selone.Tests
{
    [SetUpFixture]
    public class AssemblyTest
    {
        [OneTimeSetUp]
        public void SetUp()
        {
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            BrowserPool.Instance.Clear();
        }
    }
}