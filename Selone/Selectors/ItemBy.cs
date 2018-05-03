using System;
using OpenQA.Selenium;

namespace Kontur.Selone.Selectors
{
    public class ItemBy
    {
        public ItemBy(ByLambda preSelectorLambda, Func<IWebElement, int, By> itemSelectorFactory)
            : this(preSelectorLambda(null), itemSelectorFactory)
        {
        }

        public ItemBy(By preSelector, Func<IWebElement, int, By> itemSelectorFactory)
        {
            PreSelector = preSelector;
            ItemSelectorFactory = itemSelectorFactory;
        }

        public By PreSelector { get; }
        public Func<IWebElement, int, By> ItemSelectorFactory { get; }
    }
}