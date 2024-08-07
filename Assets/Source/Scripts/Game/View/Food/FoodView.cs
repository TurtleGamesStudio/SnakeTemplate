using Reflex.Attributes;
using Reflex.Core;
using TurtleGamesStudio.SnakeGame.Model.BoardComponents;
using UnityEngine;

namespace TurtleGamesStudio
{
    public class FoodView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private Food _food;
        private IPositionConverter _positionConverter;

        [Inject]
        private void Inject(Container container)
        {
            _positionConverter = container.Single<IPositionConverter>();
        }

        private void OnDisable()
        {
            _food.Collected -= OnCollected;
        }

        public void Init(Food food, int order)
        {
            _food = food;
            Vector3 scale = _positionConverter.ConvertScale(new Vector2(food.Diametr, food.Diametr));
            _spriteRenderer.sortingOrder = order;
            _spriteRenderer.transform.localScale = scale;
            _food.Collected += OnCollected;
        }

        private void OnCollected(Food _)
        {
            Destroy(gameObject);
        }
    }
}
