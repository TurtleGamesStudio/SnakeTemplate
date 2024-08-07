using Reflex.Attributes;
using Reflex.Core;
using TurtleGamesStudio.SnakeGame.Model;
using UnityEngine;

namespace TurtleGamesStudio
{
    public class SnakeView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private Snake _snake;
        private ThicknessAndLengthUpdater _tail;

        private IPositionConverter _viewPositionConverter;

        [Inject]
        private void Inject(Container container)
        {
            _viewPositionConverter = container.Single<IPositionConverter>();
        }

        public void Init(Snake snake, int order)
        {
            _snake = snake;
            _tail = _snake.ThicknessAndLengthUpdater;
            _spriteRenderer.sortingOrder = order;
            _snake.PositionUpdated += UpdatePositionAndRotation;
            _snake.Died += OnDied;
            _tail.ThicknessChanged += OnThicknessChanged;
            OnThicknessChanged();
        }

        public void OnDisable()
        {
            _snake.PositionUpdated -= UpdatePositionAndRotation;
            _snake.Died -= OnDied;
            _tail.ThicknessChanged -= OnThicknessChanged;
        }

        private void OnDied(Snake _)
        {
            Destroy(gameObject);
        }

        private void UpdatePositionAndRotation()
        {
            transform.position = _viewPositionConverter.ConvertPosition(_snake.transform.position);
            transform.rotation = _viewPositionConverter.ConvertRotation(_snake.transform.rotation);
        }

        private void OnThicknessChanged()
        {
            _spriteRenderer.transform.localScale = new Vector3(_tail.Thickness, _tail.Thickness, _tail.Thickness);
        }
    }
}
