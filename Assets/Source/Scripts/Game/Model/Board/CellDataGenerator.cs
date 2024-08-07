using UnityEngine;

namespace TurtleGamesStudio.SnakeGame.Model.BoardComponents
{
    public class CellDataGenerator : MonoBehaviour
    {
        [SerializeField, Min(3)] private int _width = 12;
        [SerializeField, Min(3)] private int _height = 12;

        public CellData[,] GetCells()
        {
            CellData[,] cells = new CellData[_width, _height];

            FillRow(cells, 0, CellColliderType.None, FoodSpawnType.None);

            int lastLine = cells.GetLength(1) - 1;

            for (int i = 1; i < lastLine; i++)
                FillRow(cells, i, CellColliderType.None, FoodSpawnType.Common);

            FillRow(cells, lastLine, CellColliderType.None, FoodSpawnType.None);

            return cells;
        }

        private static void FillRow(CellData[,] cells, int row,
            CellColliderType fillType, FoodSpawnType foodSpawnType)
        {
            AddCell(cells, 0, row, true, fillType, FoodSpawnType.None);

            int firstBoardCell = 1;
            int lastWallCell = cells.GetLength(0) - 1;

            for (int j = firstBoardCell; j < lastWallCell; j++)
                AddCell(cells, j, row, true, fillType, foodSpawnType);

            AddCell(cells, lastWallCell, row, true, fillType, FoodSpawnType.None);
        }

        private static void AddCell(CellData[,] cells, int column, int row, bool isFilled, CellColliderType colliderType,
            FoodSpawnType foodSpawnType)
        {
            CellData cell = new CellData(isFilled, colliderType, foodSpawnType);
            cells[column, row] = cell;
        }
    }
}
