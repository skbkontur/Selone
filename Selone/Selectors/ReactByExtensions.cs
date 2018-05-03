namespace Kontur.Selone.Selectors
{
    public static class ReactByExtensions
    {
        public static XPathBy Component(this XPathBy xPathBy, string name)
        {
            return xPathBy.WithAttribute("data-component-name", name);
        }

        public static XPathBy Component<T>(this RootXPathBy xPathBy)
        {
            return xPathBy.WithAttribute("data-component-name", typeof(T).Name);
        }

        public static XPathBy WithTid(this XPathBy xPathBy, string tid)
        {
            return xPathBy.WithAttribute("data-tid", tid);
        }

        public static XPathBy WithKey(this XPathBy xPathBy, string key)
        {
            return xPathBy.WithAttribute("data-key", key);
        }

        public static ItemBy FixedByKey(this XPathBy xPathBy)
        {
            return xPathBy.FixedByAttribute("data-key");
        }
    }
}