using System.Collections.Generic;
using Kontur.Selone.Controls.Properties;

namespace Kontur.Selone.Controls
{
    public interface IElementsCollection<out T> : IEnumerable<T>
    {
        IControlProperty<int> Count { get; }
    }
}