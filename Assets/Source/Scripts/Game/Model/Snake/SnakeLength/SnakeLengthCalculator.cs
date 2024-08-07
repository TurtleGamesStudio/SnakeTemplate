using System;

namespace TurtleGamesStudio.SnakeGame.Model
{
    public class SnakeLengthCalculator
    {
        private readonly int _minLength;
        private readonly int _massRequireForGrowth;

        public SnakeLengthCalculator(int minLength, int massRequireForGrowth)
        {
            if (minLength < 1)
                throw new ArgumentOutOfRangeException(nameof(minLength), " must be more than 0");

            if (massRequireForGrowth < 1)
                throw new ArgumentOutOfRangeException(nameof(massRequireForGrowth), " must be more than 0");

            _minLength = minLength;
            _massRequireForGrowth = massRequireForGrowth;
        }

        public int GetLength(long mass, float thickness)
        {
            return _minLength + (int)(mass / thickness / thickness / _massRequireForGrowth);
        }
    }
}
