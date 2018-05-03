using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Kontur.Selone.Controls.Properties;
using Kontur.Selone.Selectors;
using OpenQA.Selenium;

namespace Kontur.Selone.Controls
{
    public class ItemsCollection<T> : IItemsCollection<T>
    {
        private readonly ISearchContext searchContext;
        private readonly ItemBy itemBy;
        private readonly Func<ISearchContext, By, IWebElement, T> itemFactory;

        public ItemsCollection(ISearchContext searchContext, ItemByLambda itemByLambda, Func<ISearchContext, By, IWebElement, T> itemFactory)
            : this(searchContext, itemByLambda(null), itemFactory)
        {
        }

        public ItemsCollection(ISearchContext searchContext, ItemBy itemBy, Func<ISearchContext, By, IWebElement, T> itemFactory)
        {
            this.searchContext = searchContext;
            this.itemBy = itemBy;
            this.itemFactory = itemFactory;
            Count = ControlProperty.Create(() => FindElements().Count, "count");
        }

        public IControlProperty<int> Count { get; }

        public IEnumerator<T> GetEnumerator()
        {
            return FindElements().Select(CreateItem).GetEnumerator();
        }

        private IReadOnlyCollection<IWebElement> FindElements()
        {
            try
            {
                return searchContext.FindElements(itemBy.PreSelector);
            }
            catch (NoSuchElementException)
            {
                return new IWebElement[0];
            }
        }

        private T CreateItem(IWebElement element, int index)
        {
            return itemFactory(searchContext, itemBy.ItemSelectorFactory(element, index), element);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}