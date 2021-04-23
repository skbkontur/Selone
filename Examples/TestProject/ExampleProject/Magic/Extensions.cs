using System;
using System.Collections.Generic;
using System.Linq;
using Kontur.RetryableAssertions.Configuration;
using Kontur.RetryableAssertions.Extensions;
using Kontur.RetryableAssertions.ValueProviding;
using Kontur.Selone.Properties;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.Internal;
using OpenQA.Selenium;

namespace Solutions.Magic
{
    public static class Extensions
    {
        public static IValueProvider<T, T> Wait<T>(this IProp<T> prop)
        {
            return ValueProvider.Create(prop.Get, "");
        }

        public static IValueProvider<(T1, T2)[], (T1, T2)[]> Wait<T1, T2>(this IEnumerable<Props<T1, T2>> props)
        {
            return ValueProvider.Create(props.Select(x => x.Get()).ToArray, "");
        }

        public static IValueProvider<(T1, T2, T3)[], (T1, T2, T3)[]> Wait<T1, T2, T3>(this IEnumerable<Props<T1, T2, T3>> props)
        {
            return ValueProvider.Create(props.Select(x => x.Get()).ToArray, "");
        }

        public static IValueProvider<(T1, T2, T3, T4)[], (T1, T2, T3, T4)[]> Wait<T1, T2, T3, T4>(this IEnumerable<Props<T1, T2, T3, T4>> props)
        {
            return ValueProvider.Create(props.Select(x => x.Get()).ToArray, "");
        }

        public static IValueProvider<(T1, T2, T3, T4, T5)[], (T1, T2, T3, T4, T5)[]> Wait<T1, T2, T3, T4, T5>(this IEnumerable<Props<T1, T2, T3, T4, T5>> props)
        {
            return ValueProvider.Create(props.Select(x => x.Get()).ToArray, "");
        }

        public static IValueProvider<T[], T[]> Wait<T>(this IEnumerable<IProp<T>> items)
        {
            return ValueProvider.Create(items.Select(x => x.Get()).ToArray, "");
        }

        public static IValueProvider<T[], T[]> Wait<T>(this IEnumerable<T> items)
        {
            return ValueProvider.Create(items.ToArray, "");
        }

        public static IAssertionResult<T, TSource> EqualTo<T, TSource>(this IValueProvider<T, TSource> provider, T value, int? timeout = null)
        {
            return provider.That(Is.EqualTo(value), timeout);
        }

        public static IAssertionResult<T[], TSource> EquivalentTo<T, TSource>(this IValueProvider<T[], TSource> provider, IEnumerable<T> value, int? timeout = null)
        {
            return provider.That(Is.EquivalentTo(value), timeout);
        }

        public static IAssertionResult<T, TSource> That<T, TSource>(this IValueProvider<T, TSource> provider, IResolveConstraint resolveConstraint, int? timeout = null)
        {
            var constraint = new ReusableConstraint(resolveConstraint);
            var assertion = Assertion.FromDelegate<T>(x =>
            {
                using (new TestExecutionContext.IsolatedContext())
                {
                    Assert.That(x, constraint);
                }
            });

            return provider.Assert(assertion, GetConfiguration(timeout));
        }

        public static IAssertionResult<T, TSource> It<T, TSource>(this IValueProvider<T, TSource> provider, Action<T> assetion, int? timeout = null)
        {
            var assertion = Assertion.FromDelegate<T>(x =>
            {
                using (new TestExecutionContext.IsolatedContext())
                {
                    assetion(x);
                }
            });

            return provider.Assert(assertion, GetConfiguration(timeout));
        }

        public static T Single<T, TSource>(this IValueProvider<T[], TSource> provider, int? timeout = null)
        {
            return provider.Single(GetConfiguration(timeout));
        }

        public static T Single<T, TSource, TTransformed>(this IValueProvider<T[], TSource> provider, Func<T, TTransformed> transform, IResolveConstraint resolveConstraint, int? timeout = null)
        {
            var constraint = new ReusableConstraint(resolveConstraint);
            return provider.Single(x => Assert.That(transform(x), constraint), timeout);
        }

        public static T Single<T, TSource, TTransformed>(this IValueProvider<T[], TSource> provider, Func<T, IProp<TTransformed>> transform, IResolveConstraint resolveConstraint, int? timeout = null)
        {
            var constraint = new ReusableConstraint(resolveConstraint);
            return provider.Single(x => Assert.That(transform(x).Get(), constraint), timeout);
        }

        public static T Single<T, TSource>(this IValueProvider<T[], TSource> provider, Action<T> assertion, int? timeout = null)
        {
            using (new TestExecutionContext.IsolatedContext())
            {
                return provider.Single(x =>
                {
                    using (new TestExecutionContext.IsolatedContext())
                    {
                        assertion(x);
                    }
                }, GetConfiguration(timeout));
            }
        }

        private static IAssertionConfiguration GetConfiguration(int? timeout = null)
        {
            return new AssertionConfiguration
            {
                Timeout = timeout ?? 5000,
                Interval = 100,
                ExceptionMatcher = ExceptionMatcher.FromTypes(typeof(WebDriverException), typeof(PropertyTransformationException))
            };
        }
    }
}