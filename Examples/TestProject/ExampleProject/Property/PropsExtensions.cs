using Kontur.Selone.Extensions;
using Kontur.Selone.Properties;
using OpenQA.Selenium;

namespace Solutions.Property
{
    public static class PropsExtensions
    {
        public static IProp<bool> Disabled(this IWebElement webElement)
        {
            return webElement.Attribute("data-prop-disabled").Boolean();
        }

        public static IProp<bool> Checked(this IWebElement webElement)
        {
            return webElement.Attribute("data-prop-checked").Boolean();
        }

        public static IProp<bool> Boolean(this IProp<string> property)
        {
            return property.Transform(new BooleanTransformation());
        }

        public static IProp<int> Integer(this IProp<string> property)
        {
            return property.Transform(new IntegerTransformation());
        }

        public static IProp<decimal> Currency(this IProp<string> property)
        {
            return property.Transform(new CurrencyTransformation());
        }
    }
}