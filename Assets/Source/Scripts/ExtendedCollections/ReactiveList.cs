using System;
using System.Collections.Generic;

namespace TurtleGamesStudio.ExtendedCollections
{
    public class ReactiveList<T> : IReadOnlyReactiveList<T>
    {
        private readonly List<T> _list = new();

        public event Action<T> Added;
        public event Action<T> Removed;

        public IReadOnlyList<T> List => _list;

        public void Add(T item)
        {
            _list.Add(item);
            Added?.Invoke(item);
        }

        public void Remove(T item)
        {
            _list.Remove(item);
            Removed?.Invoke(item);
        }
    }
}
