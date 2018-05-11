namespace Kontur.Selone.Selectors.Css
{
    public static class ReactCssByExtensions
    {
        public static CssBy Component(this CssBy cssBy, string name)
        {
            return cssBy.WithAttribute("data-component-name", name);
        }

        public static CssBy Component<T>(this CssBy cssBy)
        {
            return cssBy.WithAttribute("data-component-name", typeof(T).Name);
        }

        public static CssBy WithTid(this CssBy cssBy, string tid)
        {
            return cssBy.WithAttribute("data-tid", tid);
        }

        public static CssBy WithKey(this CssBy cssBy, string key)
        {
            return cssBy.WithAttribute("data-key", key);
        }

        public static ItemBy FixedByKey(this CssBy cssBy)
        {
            return cssBy.FixedByAttribute("data-key");
        }
    }
}