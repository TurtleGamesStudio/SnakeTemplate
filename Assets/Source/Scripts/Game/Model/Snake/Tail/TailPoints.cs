using System;
using System.Collections.Generic;
using UnityEngine;

namespace TurtleGamesStudio.SnakeGame.Model
{
    public class TailPoints
    {
        private readonly ThicknessAndLengthUpdater _thicknessAndLengthUpdater;
        private readonly Snake _snake;
        private readonly float _previousDirectionKoefficient;
        private readonly List<Vector2> _points = new();

        public TailPoints(ThicknessAndLengthUpdater tail, Snake snake, float previousDirectionKoefficient)
        {
            _thicknessAndLengthUpdater = tail;
            _snake = snake;
            _previousDirectionKoefficient = previousDirectionKoefficient;

            AddPoints(_thicknessAndLengthUpdater.Length);
            _snake.PositionUpdated += OnPositionUpdated;
            _thicknessAndLengthUpdater.LengthChanged += OnPointCountChanged;
        }

        public event Action CountChanged;
        public event Action PositionUpdated;

        public Vector2 HeadPoint => _snake.transform.position;
        public IReadOnlyList<Vector2> Points => _points;
        public ThicknessAndLengthUpdater ThicknessAndLengthUpdater => _thicknessAndLengthUpdater;

        public void OnPointCountChanged()
        {
            int newCount = _thicknessAndLengthUpdater.Length;

            if (newCount == _points.Count)
                return;

            int difference = newCount - _points.Count;

            if (difference > 0)
                AddPoints(difference);
            else
                RemovePoints(-difference);
        }

        private void AddPoints(int count)
        {
            for (int i = 0; i < count; i++)
                AddPoint();
        }

        private void AddPoint()
        {
            Vector2 point;

            if (_points.Count == 0)
                point = _snake.transform.position;
            else
                point = _points[_points.Count - 1];

            _points.Add(point);
            CountChanged?.Invoke();
        }

        private void RemovePoints(int count)
        {
            if (count == 0 || _points.Count == 0)
                return;

            if (count > _points.Count)
                _points.RemoveRange(0, _points.Count);
            else
            {
                int index = _points.Count - count;
                _points.RemoveRange(index, count);
            }

            CountChanged?.Invoke();
        }

        private void OnPositionUpdated()
        {
            float sqrTargetRadius = _thicknessAndLengthUpdater.Thickness * _thicknessAndLengthUpdater.Thickness;
            Vector2 previousPointInPreviousFrame = _points[0];

            if (TrySetPointPosition(0, _points[0], _snake.transform.position,
                sqrTargetRadius, _thicknessAndLengthUpdater.Thickness))
            {
                for (int i = 1; i < _points.Count; i++)
                {
                    Vector2 previousPoint = _points[i - 1];
                    Vector2 nextPreviousPointInPreviousFrame = _points[i];

                    if (!TrySetPointPosition(i, previousPointInPreviousFrame,
                        previousPoint, sqrTargetRadius, _thicknessAndLengthUpdater.Thickness))
                        break;

                    previousPointInPreviousFrame = nextPreviousPointInPreviousFrame;
                }
            }

            PositionUpdated?.Invoke();
        }

        private bool TrySetPointPosition(int index, Vector2 previousPointInPreviousFrame,
            Vector2 previousPoint, float sqrTargetRadius,
            float radius)
        {
            Vector2 translation = _points[index] - previousPoint;

            if (translation.sqrMagnitude > sqrTargetRadius)
            {
                Vector2 previousDirection = (_points[index] - previousPointInPreviousFrame).normalized;
                Vector2 pullDirection = translation.normalized;
                float angle = Vector2.SignedAngle(previousDirection, pullDirection) * _previousDirectionKoefficient;
                _points[index] = previousPoint +
                    (Vector2)(Quaternion.Euler(0, 0, angle) * pullDirection * radius);
                return true;
            }

            return false;
        }
    }
}
