using System.Collections.Generic;
using System.Linq;
using TurtleGamesStudio.SnakeGame.Model.BoardComponents;
using UnityEngine;

namespace TurtleGamesStudio.SnakeGame.Model
{
    public class AvailableCellsContainer
    {
        private readonly Board _board;

        private HashSet<Vector2Int> _availableCells = new();

        public bool AvailableCellsCalculated { get; private set; } = false;

        public AvailableCellsContainer(Board board)
        {
            _board = board;
        }

        public void CalculateAvailableCells(IReadOnlyList<Snake> snakes)
        {
            if (AvailableCellsCalculated)
                return;

            _availableCells = _board.GetAvailablePositions(snakes);
            AvailableCellsCalculated = true;
        }

        public void Clear()
        {
            AvailableCellsCalculated = false;
            _availableCells.Clear();
        }

        public bool TryRemoveRandom(out Vector2Int cell)
        {
            cell = Vector2Int.zero;

            if (_availableCells.Count == 0)
                return false;

            List<Vector2Int> cellsCopy = _availableCells.ToList();
            int rand = Random.Range(0, _availableCells.Count);
            cell = cellsCopy[rand];
            _availableCells.Remove(cell);

            return true;
        }
    }
}
