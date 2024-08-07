using Reflex.Attributes;
using Reflex.Core;
using TurtleGamesStudio.ExtendedCollections;
using TurtleGamesStudio.SnakeGame.Model;
using UnityEngine;

namespace TurtleGamesStudio
{
    public class TailViewCreator : MonoBehaviour
    {
        [SerializeField] private Transform _container;

        private IReadOnlyReactiveList<Tail> _allTails;
        private TailViewSpawner _tailSpawner;

        private Container _diContainer;
        private TailView _tailTemplate;
        private IPositionConverter _positionConverter;
        private int _order;

        [Inject]
        private void Inject(Container container)
        {
            _diContainer = container;
            _tailTemplate = container.Single<TailView>();
            _positionConverter = container.Single<IPositionConverter>();
            _order = container.Single<RenderOrder>().Order[RenderName.Tail];
        }

        public void Init(IReadOnlyReactiveList<Tail> allTails)
        {
            _allTails = allTails;
            _tailSpawner = new TailViewSpawner(_tailTemplate, _positionConverter, _order,
               _diContainer, _container);

            foreach (var snake in _allTails.List)
                SpawnTail(snake);

            _allTails.Added += OnAdded;
        }

        private void OnDestroy()
        {
            _allTails.Added -= OnAdded;
        }

        private void OnAdded(Tail tail)
        {
            SpawnTail(tail);
        }

        private TailView SpawnTail(Tail tail)
        {
            return _tailSpawner.Spawn(tail);
        }
    }
}
