using Kontur.Selone.Extensions;
using OpenQA.Selenium;
using Solutions.Controls;
using Solutions.Magic;

namespace Solutions.Application
{
    public class OrderRow : ControlBase
    {
        public OrderRow(ISearchContext searchContext, By by) : base(searchContext, by)
        {
            Verified = Container.Search(x => x.WithTid("Verified")).Checkbox();
            Id = Container.Search(x => x.WithTid("Id")).Label();
            Fio = Container.Search(x => x.WithTid("Fio")).Label();
            Phone = Container.Search(x => x.WithTid("Phone")).Label();
            Sum = Container.Search(x => x.WithTid("Sum")).CurrencyLabel();
            ReloadLink = Container.Search(x => x.WithTid("ReloadLink")).Link();
            RemoveLink = Container.Search(x => x.WithTid("RemoveLink")).Link();
        }

        public Checkbox Verified { get; }
        public Label Id { get; }
        public Label Fio { get; }
        public Label Phone { get; }
        public CurrencyLabel Sum { get; }
        public Link ReloadLink { get; }
        public Link RemoveLink { get; }
    }
}