namespace Kontur.Selone.Properties
{
    public interface IPropWithTransformation<T> : IProp<T>
    {
        IProp<string> Raw { get; }
        IPropTransformation<T> Transformation { get; }
    }
}