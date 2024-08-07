using Reflex.Core;
using Reflex.Injectors;
using TurtleGamesStudio.SnakeGame.Model;
using UnityEngine;

namespace TurtleGamesStudio
{
    public class SnakeViewSpawner
    {
        private readonly SnakeView _viewTemplate;
        private readonly IPositionConverter _viewPositionConverter;
        private readonly int _order;
        private readonly Container _diContainer;
        private readonly Transform _container;

        public SnakeViewSpawner(SnakeView viewTemplate, IPositionConverter positionConverter, int order,
             Container diContainer, Transform container)
        {
            _viewTemplate = viewTemplate;
            _viewPositionConverter = positionConverter;
            _order = order;
            _diContainer = diContainer;
            _container = container;
        }

        public SnakeView Spawn(Snake snake)
        {
            Vector3 position = _viewPositionConverter.ConvertPosition(snake.transform.position);
            Quaternion rotaion = _viewPositionConverter.ConvertRotation(snake.transform.rotation);
            SnakeView snakeView = GameObject.Instantiate(_viewTemplate, position, rotaion, _container);
            AttributeInjector.Inject(snakeView, _diContainer);
            snakeView.Init(snake, _order);

            return snakeView;
        }
    }
}
