namespace Kontur.Selone.Properties
{
    public interface IPropertyTransformation<T>
    {
        T Deserialize(string value);
        string Serialize(T value);
    }
}