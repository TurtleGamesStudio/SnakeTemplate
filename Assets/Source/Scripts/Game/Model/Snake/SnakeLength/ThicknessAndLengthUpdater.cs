using System;
using UnityEngine;

namespace TurtleGamesStudio.SnakeGame.Model
{
    public class ThicknessAndLengthUpdater
    {
        private readonly Mass _mass;
        private readonly AnimationCurve _thicknessMassDependency;
        private readonly SnakeLengthCalculator _lengthCalculator;

        private float _thickness;
        private int _length;

        public ThicknessAndLengthUpdater(Mass mass, AnimationCurve thicknessMassDependency,
            SnakeLengthCalculator lengthCalculator)
        {
            _mass = mass;
            _thicknessMassDependency = thicknessMassDependency;
            _lengthCalculator = lengthCalculator;
            _mass.MassChanged += OnMassChanged;
            OnMassChanged();
        }

        public event Action ThicknessChanged;
        public event Action LengthChanged;

        public float Thickness
        {
            get => _thickness;
            set
            {
                if (_thickness != value)
                {
                    _thickness = value;
                    ThicknessChanged?.Invoke();
                }
            }
        }

        public int Length
        {
            get => _length;
            set
            {
                if (_length != value)
                {
                    _length = value;
                    LengthChanged?.Invoke();
                }
            }
        }

        private void OnMassChanged()
        {
            Thickness = _thicknessMassDependency.Evaluate(_mass.Value);
            Length = _lengthCalculator.GetLength(_mass.Value, Thickness);
        }
    }
}
