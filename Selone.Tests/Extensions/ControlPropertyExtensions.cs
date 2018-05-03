using System.Collections.Generic;
using System.Linq;
using Kontur.RetryableAssertions.ValueProviding;
using Kontur.Selone.Controls.Properties;

namespace Kontur.Selone.Tests.Extensions
{
    public static class ControlPropertyExtensions
    {
        public static IValueProvider<T, T> Wait<T>(this IControlProperty<T> controlProperty)
        {
            return ValueProvider.Create(controlProperty.Get, controlProperty.GetDescription);
        }

        public static IValueProvider<T[], T[]> Wait<T>(this IEnumerable<IControlProperty<T>> properties)
        {
            return ValueProvider.Create(() => properties.Select(x => x.Get()).ToArray(), () => "todo");
        }

        public static IValueProvider<T[], T[]> Wait<T>(this IEnumerable<T> enumerable)
        {
            return ValueProvider.Create(enumerable.ToArray, () => "todo");
        }
    }
}