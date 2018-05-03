using System;

namespace Kontur.Selone.Waiting
{
    public class WaiterCallback : IWaiter
    {
        private readonly Action<int?> action;

        public WaiterCallback(Action<int?> action)
        {
            this.action = action;
        }

        public void Wait(int? timeout)
        {
            action(timeout);
        }
    }
}