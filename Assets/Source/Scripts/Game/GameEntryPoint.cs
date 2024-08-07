using Reflex.Attributes;
using Reflex.Core;
using Reflex.Injectors;
using TurtleGamesStudio.SnakeGame.Model;
using TurtleGamesStudio.SnakeGame.Model.BoardComponents;
using UnityEngine;

namespace TurtleGamesStudio
{
    public class GameEntryPoint : MonoBehaviour
    {
        [SerializeField] private CellDataGenerator _cellDataGenerator;
        [SerializeField] private Board _board;
        [SerializeField] private RebornSystem _rebornSystem;
        [SerializeField] private TailCreator _tailCreator;
        [SerializeField] private DroppingFoodController _droppingFoodController;
        [SerializeField] private InputSpawner _inputSpawner;
        [SerializeField] private AnimationCurveSO _thicknessMassDependency;

        [Header("Views")]
        [SerializeField] private SnakeViewCreator _snakeViewCreator;
        [SerializeField] private TailViewCreator _tailViewCreator;
        [SerializeField] private FoodViewCreator _foodViewCreator;
        [SerializeField] private BoardView _boardView;

        private Container _diContainer;
        private GameMainUi _mainUi;

        [Inject]
        private void Inject(Container container)
        {
            _diContainer = container;
            _mainUi = container.Single<GameMainUi>();
        }

        private void Start()
        {
            CellData[,] cells = _cellDataGenerator.GetCells();

            AttributeInjector.Inject(_mainUi, _diContainer);
            AttributeInjector.Inject(_board, _diContainer);
            AttributeInjector.Inject(_rebornSystem, _diContainer);

            AllFood allFood = (AllFood)ConstructorInjector.Construct(typeof(AllFood), _diContainer);

            _board.Create(cells);
            _inputSpawner.Spawn();
            _rebornSystem.Init(_board, _inputSpawner.InputHandler, _inputSpawner.ClearInput,
                _thicknessMassDependency.AnimationCurve);
            _tailCreator.Init(_rebornSystem.AllSnakes);
            _droppingFoodController.Init(_rebornSystem.AllSnakes);

            _boardView.Init(_board);
            _foodViewCreator.Init(allFood.All);
            _snakeViewCreator.Init(_rebornSystem);
            _tailViewCreator.Init(_tailCreator.Tails);

            _mainUi.Init(_rebornSystem);
        }
    }
}
