using System;
using Kontur.Selone.Waiting;

namespace Kontur.Selone.Pages
{
    public static class PageExtensions
    {
        public static TPage Browse<TPage>(this TPage page, Action browse, int? timeout = null) where TPage : IPage
        {
            switch (page)
            {
                case ILoadable loadable:
                    browse();
                    loadable.WaitLoaded(timeout);
                    return page;
                case ILoadableWithWaiter loadable:
                    var waiter = loadable.BeginWaitLoaded(timeout);
                    browse();
                    waiter.Wait(timeout);
                    return page;
                default:
                    browse();
                    return page;
            }
        }

        public static TPage Refresh<TPage>(this TPage page, int? timeout = null) where TPage : IPage
        {
            return page.Browse(() => page.WrappedDriver.Navigate().Refresh(), timeout);
        }
    }
}