using System;
using Kontur.Selone.Controls;
using Kontur.Selone.Selectors;
using OpenQA.Selenium;

namespace Kontur.Selone.Tests.Controls
{
    public class ControlsCollection<T> : ItemsCollection<T>
    {
        public ControlsCollection(ISearchContext searchContext, ItemByLambda itemSelectorLambda)
            : base(searchContext, itemSelectorLambda, (sc, s, e) => (T) Activator.CreateInstance(typeof(T), sc, s))
        {
        }

        public ControlsCollection(ISearchContext searchContext, ItemByLambda itemSelectorLambda, Func<ISearchContext, By, T> itemFactory)
            : base(searchContext, itemSelectorLambda, (sc, s, e) => itemFactory(sc, s))
        {
        }
    }
}