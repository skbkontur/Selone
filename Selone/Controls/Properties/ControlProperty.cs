using System;

namespace Kontur.Selone.Controls.Properties
{
    public static class ControlProperty
    {
        public static IControlProperty<T> Create<T>(Func<T> getValue, string description)
        {
            return new ControlPropertyImplementation<T>(getValue, description);
        }

        public static IControlProperty<TNew> Then<T, TNew>(this IControlProperty<T> property, IControlProperty<TNew> then)
        {
            return property.Transform(x => then.Get());
        }

        public static IControlProperty<TNew> Then<T, TNew>(this IControlProperty<T> property, Func<IControlProperty<TNew>> then)
        {
            return property.Transform(x => then().Get());
        }

        public static IControlProperty<TNew> Transform<T, TNew>(this IControlProperty<T> property, Func<T, TNew> transform)
        {
            return new ControlPropertyImplementation<TNew>(() => transform(property.Get()), property.GetDescription());
        }

        public static IControlPropertyWithTransformation<T> Transform<T>(this IControlProperty<string> raw, IPropertyTransformation<T> transformation)
        {
            return new ControlPropertyWithTransformationImplementation<T>(raw, transformation);
        }

        public static IControlProperty<T> Check<T>(this IControlProperty<T> property, Action<T> assertion)
        {
            return new ControlPropertyImplementation<T>(() =>
            {
                var value = property.Get();
                assertion(value);
                return value;
            }, property.GetDescription());
        }

        private class ControlPropertyImplementation<T> : IControlProperty<T>
        {
            private readonly Func<T> getValue;
            private readonly string description;

            public ControlPropertyImplementation(Func<T> getValue, string description)
            {
                this.getValue = getValue;
                this.description = description;
            }

            public T Get()
            {
                return getValue();
            }

            public string GetDescription()
            {
                return description;
            }
        }

        private class ControlPropertyWithTransformationImplementation<T> : IControlPropertyWithTransformation<T>
        {
            public ControlPropertyWithTransformationImplementation(IControlProperty<string> raw, IPropertyTransformation<T> transformation)
            {
                Raw = raw;
                Transformation = transformation;
            }

            public IControlProperty<string> Raw { get; }
            public IPropertyTransformation<T> Transformation { get; }

            public T Get()
            {
                var raw = Raw.Get();
                T deserialized;
                string serialized;
                try
                {
                    deserialized = Transformation.Deserialize(raw);
                    serialized = Transformation.Serialize(deserialized);
                }
                catch (Exception e)
                {
                    throw new PropertyTransformationException($"Can not transform `{FormatValue(raw)}` to type `{typeof(T)}`", e);
                }

                if (serialized != raw)
                {
                    var message = $"Transformation mismatch.\n" +
                                  $"Target type:  {typeof(T)}\n" +
                                  $"Raw value:    {FormatValue(raw)}\n" +
                                  $"Deserialized: {FormatValue(deserialized)}\n" +
                                  $"Serialized:   {FormatValue(serialized)}";
                    throw new PropertyTransformationException(message);
                }

                return deserialized;
            }

            public string GetDescription()
            {
                return Raw.GetDescription();
            }

            private static string FormatValue(object value)
            {
                return value == null ? "<null>" : $"`{value}`";
            }
        }
    }
}