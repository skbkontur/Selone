using System.Collections.Generic;
using System.Linq;
using Kontur.RetryableAssertions.ValueProviding;
using Kontur.Selone.Properties;

namespace Kontur.Selone.Tests.Extensions
{
    public static class PropExtensions
    {
        public static IValueProvider<T, T> Wait<T>(this IProp<T> prop)
        {
            return ValueProvider.Create(prop.Get, prop.GetDescription);
        }

        public static IValueProvider<T[], T[]> Wait<T>(this IEnumerable<IProp<T>> props)
        {
            return ValueProvider.Create(() => props.Select(x => x.Get()).ToArray(), () => "todo");
        }

        public static IValueProvider<T[], T[]> Wait<T>(this IEnumerable<T> enumerable)
        {
            return ValueProvider.Create(enumerable.ToArray, () => "todo");
        }
    }
}