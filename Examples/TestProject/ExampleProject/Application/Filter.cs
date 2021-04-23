using Kontur.Selone.Extensions;
using OpenQA.Selenium;
using Solutions.Controls;
using Solutions.Magic;

namespace Solutions.Application
{
    public class Filter : ControlBase
    {
        public Filter(ISearchContext searchContext, By by) : base(searchContext, by)
        {
            SearchInput = Container.Search(x => x.WithTid("SearchInput")).Input();
            ResetButton = Container.Search(x => x.WithTid("ResetButton")).Button();
            SearchIvanButton = Container.Search(x => x.WithTid("SearchIvanButton")).Button();
            SearchTitovButton = Container.Search(x => x.WithTid("SearchTitovButton")).Button();
            SearchNothingButton = Container.Search(x => x.WithTid("SearchNothingButton")).Button();
        }

        public Input SearchInput { get; }
        public Button ResetButton { get; }
        public Button SearchIvanButton { get; }
        public Button SearchTitovButton { get; }
        public Button SearchNothingButton { get; }
    }
}