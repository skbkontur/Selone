using System.Collections.Generic;
using Kontur.Selone.Properties;

namespace Kontur.Selone.Elements
{
    public interface IElementsCollection<out T> : IEnumerable<T>
    {
        IProp<int> Count { get; }
    }
}