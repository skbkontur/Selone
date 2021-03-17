﻿using Kontur.Selone.Extensions;
using Kontur.Selone.Properties;
using Kontur.Selone.Selectors.Css;
using OpenQA.Selenium;
using Solutions.Property;

namespace Solutions.Controls
{
    public class Paging : ControlBase
    {
        public Paging(ISearchContext searchContext, By by) : base(searchContext, by)
        {
            NextLink = Container.Search(x => x.Css().WithKey("forward")).Link();
        }

        public Link NextLink { get; }
        public IProp<int> ActivePage => Container.Attribute("data-prop-ActivePage").Integer();
        public IProp<int> PagesCount => Container.Attribute("data-prop-PagesCount").Integer();

        public Link LinkTo(int page)
        {
            return Container.Search(x => x.Css().WithKey(page.ToString())).Link();
        }
    }
}