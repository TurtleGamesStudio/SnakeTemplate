using Reflex.Attributes;
using Reflex.Core;
using Reflex.Injectors;
using UnityEngine;

namespace TurtleGamesStudio.SnakeGame.Model.BoardComponents
{
    public class Cell : MonoBehaviour
    {
        public FoodController FoodController { get; private set; }

        private FoodController _foodControllerTemplate;
        private Container _diContainer;

        [Inject]
        private void Inject(Container container)
        {
            _diContainer = container;
            _foodControllerTemplate = _diContainer.Single<FoodController>();
        }

        internal void Init(CellData cell, Vector2 cellHalfSize)
        {
            if (cell.IsFilled)
            {
                if (cell.ColliderType == CellColliderType.None)
                {
                    if (cell.FoodSpawnType == FoodSpawnType.Common)
                    {
                        FoodController = Instantiate(_foodControllerTemplate, transform);
                        AttributeInjector.Inject(FoodController, _diContainer);
                        FoodController.Init(cellHalfSize);
                    }
                }
            }
        }
    }
}
