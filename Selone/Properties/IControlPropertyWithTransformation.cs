namespace Kontur.Selone.Properties
{
    public interface IControlPropertyWithTransformation<T> : IControlProperty<T>
    {
        IControlProperty<string> Raw { get; }
        IPropertyTransformation<T> Transformation { get; }
    }
}