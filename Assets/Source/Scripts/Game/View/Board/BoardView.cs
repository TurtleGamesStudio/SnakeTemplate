using Reflex.Attributes;
using Reflex.Core;
using TurtleGamesStudio.SnakeGame.Model.BoardComponents;
using UnityEngine;

namespace TurtleGamesStudio
{
    public class BoardView : MonoBehaviour
    {
        [SerializeField] private Transform _wallsContainer;

        private Board _board;
        private int _wallOrder;

        private RenderOrder _renderOrder;
        private FloorView _floorTemplate;
        private IPositionConverter _viewPositionConverter;
        private WallViewSpawner _wallViewSpawner;

        [Inject]
        private void Inject(Container container)
        {
            _renderOrder = container.Single<RenderOrder>();
            _floorTemplate = container.Single<FloorView>();

            _viewPositionConverter = container.Single<IPositionConverter>();

            _wallViewSpawner = container.Single<WallViewSpawner>();
            _wallOrder = _renderOrder.Order[RenderName.Wall];
        }

        public void Init(Board board)
        {
            _board = board;

            foreach (var wall in _board.Walls)
                CreateWall(wall);

            CreateFloor(_board.Floor);
        }

        private void CreateFloor(Floor floor)
        {
            Vector2 size = floor.Size;
            Vector2 cellSize = _board.CellSize;
            Vector3 position = _viewPositionConverter.ConvertPosition(floor.Center);
            Vector3 size3D = _viewPositionConverter.ConvertScale(size);
            FloorView view = Instantiate(_floorTemplate, position, Quaternion.identity, transform);
            view.transform.localScale = size3D;
            Vector2 materialSize = new Vector2(size.x / cellSize.x, size.y / cellSize.y);
            view.Init(materialSize, _renderOrder.Order[RenderName.Floor]);
        }

        private void CreateWall(Wall wall)
        {
            _wallViewSpawner.Spawn(wall, _wallOrder, _wallsContainer.transform);
        }
    }
}
