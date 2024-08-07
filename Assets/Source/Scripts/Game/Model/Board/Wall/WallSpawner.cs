using UnityEngine;

namespace TurtleGamesStudio.SnakeGame.Model.BoardComponents
{
    public class WallSpawner
    {
        private Wall _wallTemplate;

        public WallSpawner(Wall wallTemplate)
        {
            _wallTemplate = wallTemplate;
        }

        public Wall Spawn(Vector2 wallCenter, Vector2 wallSize, Transform container)
        {
            Wall wall = GameObject.Instantiate(_wallTemplate, wallCenter, Quaternion.identity, container);
            wall.Init(wallCenter, wallSize);
            return wall;
        }
    }
}
