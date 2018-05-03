using System;
using System.Linq;
using Kontur.RetryableAssertions.Configuration;
using Kontur.RetryableAssertions.Extensions;
using Kontur.RetryableAssertions.ValueProviding;
using Kontur.Selone.Controls.Properties;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using OpenQA.Selenium;

namespace Kontur.Selone.Tests.Extensions
{
    public static class ValueProviderExtensions
    {
        private const int DefaultTimeout = 20000;

        public static AssertionConfiguration GetConfiguration(this int? timeout)
        {
            var exceptionMatcher = ExceptionMatcher.FromTypes(typeof(WebDriverException), typeof(InvalidOperationException), typeof(PropertyTransformationException));
            var assertionConfiguration = new AssertionConfiguration {ExceptionMatcher = exceptionMatcher, Timeout = timeout ?? DefaultTimeout, Interval = 100};
            return assertionConfiguration;
        }

        //todo прокинуть timeouts
        public static IAssertionResult<T, TSource> That<T, TSource>(this IValueProvider<T, TSource> valueProvider, IResolveConstraint constraint, int? timeout = null)
        {
            return valueProvider.That(constraint, null, timeout);
        }

        //todo прокинуть timeouts
        public static IAssertionResult<T, TSource> That<T, TSource>(this IValueProvider<T, TSource> valueProvider, IResolveConstraint constraint, string message, int? timeout = null)
        {
            var reusableConstraint = new ReusableConstraint(constraint);
            var assertionDelegate = Assertion.FromDelegate<T>(x => Assert.That(x, reusableConstraint, message));
            var assertionConfiguration = timeout.GetConfiguration();
            return valueProvider.Assert(assertionDelegate, assertionConfiguration);
        }

        //todo прокинуть timeouts
        public static IAssertionResult<T, TSource> That<T, TSource>(this IValueProvider<T, TSource> valueProvider, Action<T> assertion, int? timeout = null)
        {
            var assertionDelegate = Assertion.FromDelegate(assertion);
            var assertionConfiguration = timeout.GetConfiguration();
            return valueProvider.Assert(assertionDelegate, assertionConfiguration);
        }

        public static IAssertionResult<T[], TSource> EqualTo<T, TSource>(this IValueProvider<IControlProperty<T>[], TSource> provider, T[] expected, string message = null, int? timeout = null)
        {
            return provider.Transformed(x => x.Select(i => i.Get()).ToArray()).That(Is.EqualTo(expected), message, timeout);
        }

        public static IAssertionResult<T, TSource> EqualTo<T, TSource>(this IValueProvider<T, TSource> provider, T expected, int? timeout = null)
        {
            return provider.That(Is.EqualTo(expected), timeout);
        }

        public static IAssertionResult<T, TSource> EqualTo<T, TSource>(this IValueProvider<T, TSource> provider, T expected, string message, int? timeout = null)
        {
            return provider.That(Is.EqualTo(expected), message);
        }

        public static IAssertionResult<string, TSource> Contain<TSource>(this IValueProvider<string, TSource> provider, string expected, string message = null, int? timeout = null)
        {
            return provider.That(Does.Contain(expected), message);
        }

        public static IAssertionResult<bool, TSource> True<TSource>(this IValueProvider<bool, TSource> provider, string message = null, int? timeout = null)
        {
            return provider.That(Is.EqualTo(true), message);
        }

        public static IAssertionResult<bool, TSource> False<TSource>(this IValueProvider<bool, TSource> provider, string message = null, int? timeout = null)
        {
            return provider.That(Is.EqualTo(false), message);
        }
    }
}