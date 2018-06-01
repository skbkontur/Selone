using System;

namespace Kontur.Selone.Tests.Helpers
{
    public class ClientErrorsPresentException : Exception
    {
        public ClientErrorsPresentException(string errors) : base(errors)
        {
        }
    }
}