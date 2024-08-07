using UnityEngine;

namespace TurtleGamesStudio.SnakeGame.Model.BoardComponents
{
    public class Wall : MonoBehaviour
    {
        public Vector2 WallCenter { get; private set; }
        public Vector2 WallSize { get; private set; }

        public void Init(Vector2 wallCenter, Vector2 wallSize)
        {
            WallCenter = wallCenter;
            WallSize = wallSize;
            transform.localScale = wallSize;
        }
    }
}
