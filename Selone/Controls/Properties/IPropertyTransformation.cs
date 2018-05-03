namespace Kontur.Selone.Controls.Properties
{
    public interface IPropertyTransformation<T>
    {
        T Deserialize(string value);
        string Serialize(T value);
    }
}