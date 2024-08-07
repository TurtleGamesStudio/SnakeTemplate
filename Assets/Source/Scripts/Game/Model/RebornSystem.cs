using System;
using TurtleGamesStudio.ExtendedCollections;
using TurtleGamesStudio.SnakeGame.Model.BoardComponents;
using UnityEngine;

namespace TurtleGamesStudio.SnakeGame.Model
{
    public class RebornSystem : MonoBehaviour
    {
        [SerializeField, Min(0)] private float _safeSpawnRadius = 10f;

        [SerializeField, Min(0)] private int _maxBotsCount = 30;
        [SerializeField] private BotDecisioner _botDecisionerTemplate;
        [SerializeField] private Transform _decisionerContainer;

        [SerializeField] private SnakeSpawner _snakeSpawner;
        [SerializeField, Min(0.1f)] private float _spawnInterval = 3f;

        private Board _board;
        private SnakeClearInput _playerSnakeInput;
        private IInputHandler _playerInput;
        private float _sqrSafeSpawnRadius;
        private float _time;
        private AvailableCellsContainer _availableCellsContainer;
        private ReactiveList<Snake> _bots = new();
        private ReactiveList<Snake> _allSnakes = new();

        public event Action GameLost;
        public event Action GameRestarted;

        public IReadOnlyReactiveList<Snake> AllSnakes => _allSnakes;
        public IReadOnlyReactiveList<Snake> Bots => _bots;
        public Snake Player { get; private set; }

        public void Init(Board board, IInputHandler playerInput,
            SnakeClearInput playerSnakeInput, AnimationCurve thicknessMassDependency)
        {
            _board = board;
            _playerInput = playerInput;
            _playerSnakeInput = playerSnakeInput;
            _sqrSafeSpawnRadius = _safeSpawnRadius * _safeSpawnRadius;
            _snakeSpawner.Init(thicknessMassDependency);
            _availableCellsContainer = new AvailableCellsContainer(_board);

            _availableCellsContainer.CalculateAvailableCells(_allSnakes.List);
            SpawnPlayer();
            SpawnBots(_maxBotsCount);
            _availableCellsContainer.Clear();
        }

        private void OnDisable()
        {
            foreach (var snake in _allSnakes.List)
                snake.Died -= OnSnakeDied;

            foreach (var snake in _bots.List)
                snake.Died -= OnBotDied;

            if (Player != null)
                Player.Died -= OnPlayerDied;
        }

        private void Update()
        {
            _time += Time.deltaTime;

            if (_time >= _spawnInterval)
            {
                _time -= _spawnInterval;

                int count = _maxBotsCount - _bots.List.Count;

                if (count > 0)
                    RespawnBots(count);
            }
        }

        public void RespawnPlayer()
        {
            _availableCellsContainer.CalculateAvailableCells(_allSnakes.List);
            SpawnPlayer();
            _availableCellsContainer.Clear();
            GameRestarted?.Invoke();
        }

        private void SpawnPlayer()
        {
            Vector2 position = GetSpawnPosition();
            Player = _snakeSpawner.Spawn(position, _playerSnakeInput);
            Player.name = "Player";

            _playerInput.SetSnake(Player);

            _allSnakes.Add(Player);
            Player.Died += OnSnakeDied;

            Player.Died += OnPlayerDied;
        }

        private void OnBotDied(Snake snake)
        {
            snake.Died -= OnBotDied;
            _bots.Remove(snake);
        }

        private void OnSnakeDied(Snake snake)
        {
            snake.Died -= OnSnakeDied;
            _allSnakes.Remove(snake);
        }

        private void OnPlayerDied(Snake _)
        {
            Player.Died -= OnPlayerDied;
            _playerInput.RemoveSnake();
            GameLost?.Invoke();
        }

        private void RespawnBots(int count)
        {
            _availableCellsContainer.CalculateAvailableCells(_allSnakes.List);
            SpawnBots(count);
            _availableCellsContainer.Clear();
        }

        private void SpawnBots(int count)
        {
            for (int i = 0; i < count; i++)
            {
                if (_availableCellsContainer.TryRemoveRandom(out Vector2Int cell))
                    SpawnBot(cell);
                else
                    break;
            }
        }

        private void SpawnBot(Vector2Int cell)
        {
            Vector2 spawnPosition = _board.GetWorldPosition(cell);
            SnakeClearInput input = new SnakeClearInput();
            Snake snake;
            snake = _snakeSpawner.Spawn(spawnPosition, input);
            BotDecisioner botDecisioner = Instantiate(_botDecisionerTemplate, _decisionerContainer);
            botDecisioner.Init(input, snake, _board);

            _allSnakes.Add(snake);
            snake.Died += OnSnakeDied;

            _bots.Add(snake);
            snake.Died += OnBotDied;
        }

        private Vector2 GetSpawnPosition()
        {
            Vector2 spawnPosition;

            if (_availableCellsContainer.TryRemoveRandom(out Vector2Int cell))
            {
                spawnPosition = _board.GetWorldPosition(cell);
            }
            else
            {
                Vector2Int randomCell = _board.GetRandomBornableCell();
                spawnPosition = _board.GetWorldPosition(randomCell);

                RemoveBots(spawnPosition);
            }

            return spawnPosition;
        }

        private void RemoveBots(Vector2 center)
        {
            for (int i = _bots.List.Count - 1; i < -1; i--)
            {
                Snake bot = _bots.List[i];

                if (IsInCircle(bot, center, _sqrSafeSpawnRadius))
                    bot.Die();
            }
        }

        private bool IsInCircle(Snake snake, Vector2 center, float sqrRadius)
        {
            if (((Vector2)snake.transform.position - center).sqrMagnitude < sqrRadius)
            {
                return true;
            }
            else
            {
                for (int i = 5; i < snake.TailPoints.Points.Count; i += 5)
                    if ((snake.TailPoints.Points[i] - center).sqrMagnitude < sqrRadius)
                        return true;
            }

            return false;
        }
    }
}
