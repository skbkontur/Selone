namespace Kontur.Selone.Controls.Properties
{
    public interface IControlProperty<out T>
    {
        T Get();
        string GetDescription();
    }
}