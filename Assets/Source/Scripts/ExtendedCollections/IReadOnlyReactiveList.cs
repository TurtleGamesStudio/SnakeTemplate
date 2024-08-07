using System;
using System.Collections.Generic;

namespace TurtleGamesStudio.ExtendedCollections
{
    public interface IReadOnlyReactiveList<T>
    {
        event Action<T> Added;
        event Action<T> Removed;

        IReadOnlyList<T> List { get; }
    }
}
