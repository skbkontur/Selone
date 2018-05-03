namespace Kontur.Selone.Waiting
{
    public interface IWaiter
    {
        void Wait(int? timeout);
    }
}