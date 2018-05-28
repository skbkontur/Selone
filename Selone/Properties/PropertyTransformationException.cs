using System;

namespace Kontur.Selone.Properties
{
    public class PropertyTransformationException : Exception
    {
        public PropertyTransformationException(string message) : base(message)
        {
        }

        public PropertyTransformationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}