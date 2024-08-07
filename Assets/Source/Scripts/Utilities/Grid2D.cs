using UnityEngine;

namespace TurtleGamesStudio.Utilities
{
    public static class Grid2D
    {
        public static Vector2 GetWorldPosition(Vector2Int gridPosition, Vector2 cellSize, Vector2 halfCellSize)
        {
            float positionX = GetWorldDimension(gridPosition.x, cellSize.x, halfCellSize.x);
            float positionY = GetWorldDimension(gridPosition.y, cellSize.y, halfCellSize.y);
            return new Vector2(positionX, positionY);
        }

        public static Vector2Int GetGrid2DPosition(Vector2 worldPosition, Vector2 cellSize)
        {
            int positionX = GetGridDimension(worldPosition.x, cellSize.x);
            int positionY = GetGridDimension(worldPosition.y, cellSize.y);

            return new Vector2Int(positionX, positionY);
        }

        public static int GetGridDimension(float worldDimension, float cellDimension)
        {
            int wholePart = (int)(worldDimension / cellDimension);
            return worldDimension < 0 ? wholePart - 1 : wholePart;
        }

        public static float GetWorldDimension(int gridDimesion, float size, float halfSize)
        {
            return gridDimesion * size + halfSize;
        }
    }
}
