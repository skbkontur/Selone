using System.Globalization;
using Kontur.Selone.Properties;

namespace Solutions.Property
{
    public class CurrencyTransformation : IPropTransformation<decimal>
    {
        public decimal Deserialize(string value)
        {
            return decimal.Parse(value, new CultureInfo("ru-RU"));
        }

        public string Serialize(decimal value)
        {
            return value.ToString(new CultureInfo("ru-RU"));
        }
    }
}