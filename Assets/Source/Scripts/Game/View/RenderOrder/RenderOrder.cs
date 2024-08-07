using System.Collections.Generic;
using UnityEngine;

namespace TurtleGamesStudio
{
    [CreateAssetMenu(fileName = nameof(RenderOrder), menuName = nameof(RenderOrder))]
    public class RenderOrder : ScriptableObject
    {
        [SerializeField] private RenderName[] _orderedNames;
        [field: SerializeField] public int Initial { get; private set; } = -1000000;
        [field: SerializeField] public int Step { get; private set; } = 100;

        private readonly Dictionary<RenderName, int> _order = new();

        public IReadOnlyDictionary<RenderName, int> Order => _order;

        public void Init()
        {
            foreach (var orderName in _orderedNames)
            {
                int value = Initial + Step * _order.Count;
                _order.TryAdd(orderName, value);
            }
        }
    }
}