using Kontur.Selone.Elements;
using Kontur.Selone.Selectors.Css;
using OpenQA.Selenium;
using Solutions.Controls;
using Solutions.Magic;

namespace Solutions.Application
{
    public class OrderTable : ControlBase
    {
        public OrderTable(ISearchContext searchContext, By by) : base(searchContext, by)
        {
            Items = new ElementsCollection<OrderRow>(Container, x => x.WithTid("Order").FixedByKey(), (sc, selector, we) => new OrderRow(sc, selector));
        }

        public IElementsCollection<OrderRow> Items { get; }
    }
}