namespace Kontur.Selone.Properties
{
    public interface IControlProperty<out T>
    {
        T Get();
        string GetDescription();
    }
}