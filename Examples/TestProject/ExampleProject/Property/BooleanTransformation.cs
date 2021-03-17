using Kontur.Selone.Properties;

namespace Solutions.Property
{
    public class BooleanTransformation : IPropTransformation<bool>
    {
        public bool Deserialize(string value)
        {
            return bool.Parse(value);
        }

        public string Serialize(bool value)
        {
            return value.ToString().ToLower();
        }
    }
}