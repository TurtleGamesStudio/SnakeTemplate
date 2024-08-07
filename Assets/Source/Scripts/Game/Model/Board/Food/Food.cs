using System;
using UnityEngine;

namespace TurtleGamesStudio.SnakeGame.Model.BoardComponents
{
    public class Food : MonoBehaviour
    {
        private CircleCollider2D _circleCollider;

        public event Action<Food> Collected;

        public int Mass { get; private set; }
        public float Diametr { get; private set; }

        public void Init(int mass, float radius)
        {
            Mass = mass;
            Diametr = radius * 2;
            _circleCollider = GetComponent<CircleCollider2D>();
            _circleCollider.radius = radius;
        }

        public void Collect()
        {
            Collected?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
