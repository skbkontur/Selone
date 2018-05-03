using System;
using Kontur.Selone.Helpers;
using Kontur.Selone.Waiting;

namespace Kontur.Selone.Pages
{
    public static class PageExtensions
    {
        public static TPage Browse<TPage>(this TPage page, Action browse, int? timeout = null) where TPage : IPage
        {
            page.AssertErrorsAbsent(() =>
            {
                switch (page)
                {
                    case ILoadable loadable:
                        browse();
                        loadable.WaitLoaded(timeout);
                        return;
                    case ILoadableWithWaiter loadable:
                        var waiter = loadable.BeginWaitLoaded(timeout);
                        browse();
                        waiter.Wait(timeout);
                        return;
                    default:
                        browse();
                        return;
                }
            });
            return page;
        }

        public static TPage Refresh<TPage>(this TPage page, int? timeout = null) where TPage : IPage
        {
            return page.Browse(() => page.WrappedDriver.Navigate().Refresh(), timeout);
        }

        private static void AssertErrorsAbsent<TPage>(this TPage page, Action action) where TPage : IPage
        {
            page.WrappedDriver.AssertClientErrorsAbsent();
            action();
            page.WrappedDriver.AssertClientErrorsAbsent();
        }
    }
}