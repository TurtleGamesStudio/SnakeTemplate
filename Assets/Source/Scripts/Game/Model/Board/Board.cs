using System.Collections.Generic;
using Reflex.Attributes;
using Reflex.Core;
using Reflex.Injectors;
using TurtleGamesStudio.Utilities;
using UnityEngine;

namespace TurtleGamesStudio.SnakeGame.Model.BoardComponents
{
    public class Board : MonoBehaviour
    {
        [SerializeField] private Transform _wallsContainer;
        [SerializeField] private Transform _cellsContainer;

        [SerializeField, Min(1)] private float _size = 5f;

        private CellData[,] _cells;
        private Vector2 _cellSize;
        private Vector2 _halfCellSize;

        private List<Wall> _walls = new();
        private HashSet<Vector2Int> _spawnableCells = new();

        public Floor Floor { get; private set; }
        public Vector2 Center { get; private set; }
        public Vector2 CellSize => _cellSize;
        public Vector2 HalfCellSize => _halfCellSize;
        public IReadOnlyList<Vector2Int> SpawnableCells => (IReadOnlyList<Vector2Int>)_spawnableCells;
        public IReadOnlyList<Wall> Walls => _walls;

        private Container _diContainer;
        private Floor _floorTemplate;
        private Cell _cellTemplate;
        private WallSpawner _wallSpawner;

        [Inject]
        private void Inject(Container container)
        {
            _diContainer = container;
            _floorTemplate = container.Single<Floor>();
            _cellTemplate = container.Single<Cell>();
            _wallSpawner = container.Single<WallSpawner>();
        }

        public void Create(CellData[,] cells)
        {
            _cells = cells;
            _cellSize = new Vector2(_size, _size);
            _halfCellSize = _cellSize / 2;

            for (int i = 0; i < _cells.GetLength(0); i++)
            {
                for (int j = 0; j < _cells.GetLength(1); j++)
                {
                    CellData cell = _cells[i, j];
                    CreateCell(i, j, cell);

                    if (cell.IsFilled &&
                        cell.ColliderType == CellColliderType.None &&
                        cell.FoodSpawnType == FoodSpawnType.Common)
                        _spawnableCells.Add(new Vector2Int(i, j));
                }
            }

            float fieldSizeX = _cells.GetLength(0) * _size;
            float fieldSizeY = _cells.GetLength(1) * _size;
            Vector2 fieldSize = new Vector2(fieldSizeX, fieldSizeY);

            float fieldCenterX = fieldSizeX / 2f;
            float fieldCenterY = fieldSizeY / 2f;
            Vector2 fieldCenter = new Vector2(fieldCenterX, fieldCenterY);
            Center = fieldCenter;

            CreateExternalWalls(fieldCenter);

            CreateFloor(fieldCenter, fieldSize);
        }

        public Vector2 GetWorldPosition(Vector2Int cellPosition)
        {
            return Grid2D.GetWorldPosition(cellPosition, _cellSize, _halfCellSize);
        }

        public HashSet<Vector2Int> GetAvailablePositions(IReadOnlyList<Snake> snakes)
        {
            HashSet<Vector2Int> cellsAvailableForSpawn = new HashSet<Vector2Int>(_spawnableCells);

            HashSet<Vector2Int> lockForSpawn = new HashSet<Vector2Int>();

            foreach (var snake in snakes)
            {
                AddCell(lockForSpawn, snake.transform.position);

                for (int i = 5; i < snake.TailPoints.Points.Count; i += 5)
                    AddCell(lockForSpawn, snake.TailPoints.Points[i]);
            }

            foreach (var cell in lockForSpawn)
                cellsAvailableForSpawn.Remove(cell);

            return cellsAvailableForSpawn;
        }

        public Vector2Int GetRandomBornableCell()
        {
            int rand = Random.Range(0, SpawnableCells.Count);
            return SpawnableCells[rand];
        }

        private Cell CreateCell(int column, int row, CellData cellData)
        {
            float positionX = Grid2D.GetWorldDimension(column, _size, _halfCellSize.x);
            float positionY = Grid2D.GetWorldDimension(row, _size, _halfCellSize.y);
            Vector2 spawnPosition = new Vector2(positionX, positionY);
            Cell cell = Instantiate(_cellTemplate, spawnPosition,
                Quaternion.identity, _cellsContainer.transform);
            AttributeInjector.Inject(cell, _diContainer);
            cell.Init(cellData, _halfCellSize);
            return cell;
        }

        private void AddCell(HashSet<Vector2Int> hashSet, Vector2 position)
        {
            Vector2Int gridPosition = Grid2D.GetGrid2DPosition(position, _cellSize);
            hashSet.Add(gridPosition);
        }

        private void CreateFloor(Vector2 fieldCenter, Vector2 size)
        {
            Floor = Instantiate(_floorTemplate, fieldCenter, Quaternion.identity, transform);
            Floor.Init(fieldCenter, size);
        }

        #region Walls
        private void CreateExternalWalls(Vector2 fieldCenter)
        {
            Vector2 verticalWallSize = new Vector2(_size, _cells.GetLength(1) * _size);

            float left = Grid2D.GetWorldDimension(-1, _size, _halfCellSize.x);
            float right = Grid2D.GetWorldDimension(_cells.GetLength(0), _size, _halfCellSize.x);
            float bottom = Grid2D.GetWorldDimension(-1, _size, _halfCellSize.y);
            float top = Grid2D.GetWorldDimension(_cells.GetLength(1), _size, _halfCellSize.y);

            Vector2 leftWallCenter = new Vector2(left, fieldCenter.y);
            Vector2 rightWallCenter = new Vector2(right, fieldCenter.y);

            CreateWall(leftWallCenter, verticalWallSize);
            CreateWall(rightWallCenter, verticalWallSize);

            Vector2 horizontalWallSize = new Vector2((_cells.GetLength(0) + 2) * _size, _size);

            Vector2 bottomWallCenter = new Vector2(fieldCenter.x, bottom);
            Vector2 topWallCenter = new Vector2(fieldCenter.x, top);

            CreateWall(bottomWallCenter, horizontalWallSize);
            CreateWall(topWallCenter, horizontalWallSize);
        }

        private void CreateWall(Vector2 wallCenter, Vector2 wallSize)
        {
            Wall wall = _wallSpawner.Spawn(wallCenter, wallSize, _wallsContainer);
            _walls.Add(wall);
        }
        #endregion
    }
}
