using Kontur.Selone.Selectors;
using Kontur.Selone.Selectors.Css;

namespace Solutions.Magic
{
    public static class SelectorExtensions
    {
        public static CssBy WithTid(this ByDummy dummy, string tid)
        {
            return dummy.Css().WithTid(tid);
        }
    }
}