namespace Kontur.Selone.Properties
{
    public interface IPropTransformation<T>
    {
        T Deserialize(string value);
        string Serialize(T value);
    }
}