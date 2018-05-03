using System;

namespace Kontur.Selone.Controls.Properties
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