using System;

namespace TurtleGamesStudio.SnakeGame.Model
{
    public class Mass
    {
        private long _value;

        public Mass(long value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value), " must be non negative.");

            _value = value;
        }

        public event Action MassChanged;

        public long Value
        {
            get => _value;

            private set
            {
                _value = value;
                MassChanged?.Invoke();
            }
        }

        public void AddMass(int mass)
        {
            if (mass < 0)
                throw new ArgumentOutOfRangeException(nameof(mass), " must be non negative.");

            Value += mass;
        }

        public void DecreaseMass()
        {
            throw new NotImplementedException("Weight reduction not implemented");
        }
    }
}
