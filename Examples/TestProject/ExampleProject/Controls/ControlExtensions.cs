using System;
using Kontur.Selone.Selectors.Context;

namespace Solutions.Controls
{
    public static class ControlExtensions
    {
        public static T Control<T>(this IContextBy contextBy) where T : ControlBase
        {
            return (T) Activator.CreateInstance(typeof(T), contextBy.SearchContext, contextBy.By);
        }

        public static Button Button(this IContextBy contextBy)
        {
            return new Button(contextBy.SearchContext, contextBy.By);
        }

        public static Link Link(this IContextBy contextBy)
        {
            return new Link(contextBy.SearchContext, contextBy.By);
        }

        public static Input Input(this IContextBy contextBy)
        {
            return new Input(contextBy.SearchContext, contextBy.By);
        }

        public static Checkbox Checkbox(this IContextBy contextBy)
        {
            return new Checkbox(contextBy.SearchContext, contextBy.By);
        }

        public static Label Label(this IContextBy contextBy)
        {
            return new Label(contextBy.SearchContext, contextBy.By);
        }

        public static CurrencyLabel CurrencyLabel(this IContextBy contextBy)
        {
            return new CurrencyLabel(contextBy.SearchContext, contextBy.By);
        }
    }
}