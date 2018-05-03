namespace Kontur.Selone.Waiting
{
    public interface ILoadableWithWaiter
    {
        IWaiter BeginWaitLoaded(int? timeout = null);
    }
}