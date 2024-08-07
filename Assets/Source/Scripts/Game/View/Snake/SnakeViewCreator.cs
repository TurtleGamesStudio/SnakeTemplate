using Cinemachine;
using Reflex.Attributes;
using Reflex.Core;
using TurtleGamesStudio.ExtendedCollections;
using TurtleGamesStudio.SnakeGame.Model;
using UnityEngine;

namespace TurtleGamesStudio
{
    public class SnakeViewCreator : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        [SerializeField] private Vector3 _baseFollowOffset = new Vector3(0, 0, -18);

        private IReadOnlyReactiveList<Snake> _bots;
        private RebornSystem _rebornSystem;

        private CinemachineVirtualCamera _camera;
        private SnakeViewSpawner _snakeViewSpawner;

        [Inject]
        private void Inject(Container container)
        {
            _camera = container.Single<GameCameras>().SnakeCamera;

            SnakeView snakeView = container.Single<SnakeView>();
            IPositionConverter positionConverter = container.Single<IPositionConverter>();
            int order = container.Single<RenderOrder>().Order[RenderName.Head];
            _snakeViewSpawner = new SnakeViewSpawner(snakeView, positionConverter, order, container, _container);
        }

        public void Init(RebornSystem rebornSystem)
        {
            _rebornSystem = rebornSystem;
            _bots = _rebornSystem.Bots;

            foreach (var bot in _bots.List)
                SpawnSnakeView(bot);

            OnPlayerSpawned();

            _bots.Added += OnBotAdded;
            _rebornSystem.GameRestarted += OnPlayerSpawned;

            new SnakeCameraController(_rebornSystem, _baseFollowOffset, _camera);
        }

        private void OnDestroy()
        {
            _bots.Added -= OnBotAdded;
            _rebornSystem.GameRestarted -= OnPlayerSpawned;
        }

        private void OnPlayerSpawned()
        {
            SnakeView snakeView = SpawnSnakeView(_rebornSystem.Player);

            _camera.LookAt = snakeView.transform;
            _camera.Follow = snakeView.transform;
        }

        private void OnBotAdded(Snake bot)
        {
            SpawnSnakeView(bot);
        }

        private SnakeView SpawnSnakeView(Snake snake)
        {
            return _snakeViewSpawner.Spawn(snake);
        }
    }
}
