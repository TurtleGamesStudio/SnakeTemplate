using Reflex.Attributes;
using Reflex.Core;
using TurtleGamesStudio.ExtendedCollections;
using UnityEngine;

namespace TurtleGamesStudio.SnakeGame.Model
{
    public class TailCreator : MonoBehaviour
    {
        [SerializeField] private Transform _container;

        private readonly ReactiveList<Tail> _tails = new();
        private IReadOnlyReactiveList<Snake> _allSnakes;
        private TailSpawner _tailSpawner;

        public IReadOnlyReactiveList<Tail> Tails => _tails;

        private Tail _tailTemplate;

        [Inject]
        private void Inject(Container container)
        {
            _tailTemplate = container.Single<Tail>();
        }

        public void Init(IReadOnlyReactiveList<Snake> allSnakes)
        {
            _allSnakes = allSnakes;
            _tailSpawner = new TailSpawner(_tailTemplate, _container);

            foreach (var snake in _allSnakes.List)
                SpawnTail(snake);

            _allSnakes.Added += OnAdded;
        }

        private void OnDestroy()
        {
            _allSnakes.Added -= OnAdded;
        }

        private void OnAdded(Snake snake)
        {
            SpawnTail(snake);
        }

        private Tail SpawnTail(Snake snake)
        {
            Tail tail = _tailSpawner.Spawn(snake);
            _tails.Add(tail);
            return tail;
        }
    }
}
