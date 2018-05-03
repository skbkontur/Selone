namespace Kontur.Selone.Waiting
{
    public interface ILoadable
    {
        void WaitLoaded(int? timeout = null);
    }
}