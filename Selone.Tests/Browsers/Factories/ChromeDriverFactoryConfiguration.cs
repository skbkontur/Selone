namespace Kontur.Selone.Tests.Browsers.Factories
{
    public class ChromeDriverFactoryConfiguration
    {
        private WindowSize windowSize;

        public string ChromeDirectoryPath { get; set; }

        public string DriverDirectoryPath { get; set; }

        public WindowSize WindowSize { get => windowSize ?? (windowSize = new WindowSize()); set => windowSize = value; }
    }
}