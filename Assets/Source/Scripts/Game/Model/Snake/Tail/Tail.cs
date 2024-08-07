using System;
using System.Linq;
using UnityEngine;

namespace TurtleGamesStudio.SnakeGame.Model
{
    public class Tail : MonoBehaviour
    {
        [SerializeField] private EdgeCollider2D _edgeCollider;

        private TailPoints _tailPoints;
        private ThicknessAndLengthUpdater _tail;

        public event Action Died;

        public Snake Owner { get; private set; }
        public TailPoints TailPoints => _tailPoints;
        public ThicknessAndLengthUpdater ThicknessAndLengthUpdater => _tail;

        public void Init(TailPoints tailPoints,
            ThicknessAndLengthUpdater tail, Snake owner)
        {
            _tailPoints = tailPoints;
            _tail = tail;
            Owner = owner;

            Owner.Died += OnDied;
            _tailPoints.PositionUpdated += OnPositionUpdated;
            _tail.ThicknessChanged += OnThicknessChanged;
        }

        private void OnDisable()
        {
            Owner.Died -= OnDied;
            _tailPoints.PositionUpdated -= OnPositionUpdated;
            _tail.ThicknessChanged -= OnThicknessChanged;
        }

        private void OnPositionUpdated()
        {
            _edgeCollider.points = _tailPoints.Points.ToArray();
        }

        private void OnThicknessChanged()
        {
            _edgeCollider.edgeRadius = _tail.Thickness;
        }

        private void OnDied(Snake _)
        {
            Died?.Invoke();
            Destroy(gameObject);
        }
    }
}
