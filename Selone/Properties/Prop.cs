using System;

namespace Kontur.Selone.Properties
{
    public static class Prop
    {
        public static IProp<T> Create<T>(Func<T> getValue, string description)
        {
            return new PropImplementation<T>(getValue, description);
        }

        public static IProp<TNew> Then<T, TNew>(this IProp<T> prop, IProp<TNew> then)
        {
            return prop.Transform(x => then.Get());
        }

        public static IProp<TNew> Then<T, TNew>(this IProp<T> prop, Func<IProp<TNew>> then)
        {
            return prop.Transform(x => then().Get());
        }

        public static IProp<TNew> Transform<T, TNew>(this IProp<T> prop, Func<T, TNew> transform)
        {
            return new PropImplementation<TNew>(() => transform(prop.Get()), prop.GetDescription());
        }

        public static IPropWithTransformation<T> Transform<T>(this IProp<string> raw, IPropTransformation<T> transformation)
        {
            return new PropWithTransformationImplementation<T>(raw, transformation);
        }

        public static IProp<T> Check<T>(this IProp<T> prop, Action<T> assertion)
        {
            return new PropImplementation<T>(() =>
            {
                var value = prop.Get();
                assertion(value);
                return value;
            }, prop.GetDescription());
        }

        private class PropImplementation<T> : IProp<T>
        {
            private readonly Func<T> getValue;
            private readonly string description;

            public PropImplementation(Func<T> getValue, string description)
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

        private class PropWithTransformationImplementation<T> : IPropWithTransformation<T>
        {
            public PropWithTransformationImplementation(IProp<string> raw, IPropTransformation<T> transformation)
            {
                Raw = raw;
                Transformation = transformation;
            }

            public IProp<string> Raw { get; }
            public IPropTransformation<T> Transformation { get; }

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