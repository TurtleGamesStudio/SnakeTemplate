using System.Collections.Generic;
using TurtleGamesStudio.SnakeGame.Model;
using UnityEngine;

namespace TurtleGamesStudio
{
    public class TailView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _segmentTemplate;

        private Tail _tail;
        private IPositionConverter _viewPositionConverter;
        private int _order;

        private TailPoints _tailPoints;
        private ThicknessAndLengthUpdater _thicknessAndLengthUpdater;
        private readonly List<SpriteRenderer> _segments = new();

        public void Init(Tail tail, IPositionConverter viewPositionConverter,
            int order)
        {
            _tail = tail;
            _order = order;
            _tailPoints = tail.TailPoints;
            _thicknessAndLengthUpdater = tail.ThicknessAndLengthUpdater;
            _viewPositionConverter = viewPositionConverter;

            _tailPoints.PositionUpdated += OnPositionUpdated;
            _thicknessAndLengthUpdater.ThicknessChanged += OnThicknessChanged;
            _tail.Died += OnDied;

            OnPositionUpdated();
            OnThicknessChanged();
        }

        private void OnDisable()
        {
            _tail.Died -= OnDied;
        }

        private void OnPositionUpdated()
        {
            List<Vector3> positions = _viewPositionConverter.ConvertPositions(_tailPoints.Points);

            while (positions.Count > _segments.Count)
                AddSegment();

            while (positions.Count < _segments.Count)
                RemoveSegment();

            SetPositionAndRotation(0, _tailPoints.HeadPoint, positions);

            for (int i = 1; i < positions.Count; i++)
            {
                Vector2 previousPosition = positions[i - 1];
                SetPositionAndRotation(i, previousPosition, positions);
            }
        }

        private void SetPositionAndRotation(int point, Vector2 previousPosition, IReadOnlyList<Vector3> positions)
        {
            _segments[point].transform.position = positions[point];
            SetRotation(point, previousPosition);
        }

        private void SetRotation(int point, Vector2 previousPosition)
        {
            Vector2 direction = GetDirection(previousPosition, _tailPoints.Points[point]);
            Quaternion modelRotation = Quaternion.FromToRotation(Vector2.up, direction);
            Quaternion rotation = _viewPositionConverter.ConvertRotation(modelRotation);
            _segments[point].transform.rotation = rotation;
        }

        private Vector2 GetDirection(Vector2 previousPosition, Vector2 targetPoisition)
        {
            return previousPosition - targetPoisition;
        }

        private void AddSegment()
        {
            SpriteRenderer segment = Instantiate(_segmentTemplate, transform);
            segment.sortingOrder = _order;
            Vector3 scale = GetScale();
            SetThickness(segment, scale);
            _segments.Add(segment);
        }

        private void RemoveSegment()
        {
            int index = _segments.Count - 1;
            SpriteRenderer segment = _segments[index];
            _segments.RemoveAt(index);
            Destroy(segment.gameObject);
        }

        private void OnThicknessChanged()
        {
            Vector3 scale = GetScale();

            foreach (var segment in _segments)
                SetThickness(segment, scale);
        }

        private Vector3 GetScale()
        {
            float diametr = _thicknessAndLengthUpdater.Thickness * 2f;
            return _viewPositionConverter.
                  ConvertScale(new Vector2(diametr, diametr));
        }

        private void SetThickness(SpriteRenderer segment, Vector3 scale)
        {
            segment.transform.localScale = scale;
        }

        private void OnDied()
        {
            Destroy(gameObject);
        }
    }
}
