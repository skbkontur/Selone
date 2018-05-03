using System;

namespace Kontur.Selone.Helpers
{
    public class ClientErrorsPresentException : Exception
    {
        public ClientErrorsPresentException(string errors) : base(errors)
        {
        }
    }
}