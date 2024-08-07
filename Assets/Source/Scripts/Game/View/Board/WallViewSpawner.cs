using TurtleGamesStudio.SnakeGame.Model.BoardComponents;
using UnityEngine;

namespace TurtleGamesStudio
{
    public class WallViewSpawner
    {
        private readonly IPositionConverter _viewPositionConverter;
        private readonly WallView _wallViewTemplate;

        public WallViewSpawner(IPositionConverter viewPositionConverter, WallView wallViewTemplate)
        {
            _viewPositionConverter = viewPositionConverter;
            _wallViewTemplate = wallViewTemplate;
        }

        public void Spawn(Wall wall, int order, Transform viewContainer)
        {
            Vector3 spawnPoint = _viewPositionConverter.ConvertPosition(wall.WallCenter);
            Vector3 scale = _viewPositionConverter.ConvertScale(wall.WallSize);
            WallView wallView = GameObject.Instantiate(_wallViewTemplate, spawnPoint, Quaternion.identity, viewContainer);
            wallView.transform.localScale = scale;
            wallView.Init(order);
        }
    }
}
