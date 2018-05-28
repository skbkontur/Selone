namespace Kontur.Selone.Properties
{
    public interface IProp<out T>
    {
        T Get();
        string GetDescription();
    }
}