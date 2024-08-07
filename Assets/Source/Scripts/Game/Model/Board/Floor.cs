using UnityEngine;

namespace TurtleGamesStudio.SnakeGame.Model.BoardComponents
{
    public class Floor : MonoBehaviour
    {
        public Vector2 Center { get; private set; }
        public Vector2 Size { get; private set; }

        public void Init(Vector2 wallCenter, Vector2 wallSize)
        {
            Center = wallCenter;
            Size = wallSize;
            transform.localScale = wallSize;
        }
    }
}
