namespace Kontur.Selone.Tests.Browsers.Factories
{
    public class ChromeDriverFactoryConfiguration
    {
        private WindowSize windowSize;

        public WindowSize WindowSize { get => windowSize ?? (windowSize = new WindowSize()); set => windowSize = value; }
    }
}