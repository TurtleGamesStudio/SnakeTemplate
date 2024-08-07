using System;
using UnityEngine;

namespace TurtleGamesStudio.SnakeGame.Model
{
    public class TailSpawner
    {
        private readonly Tail _tailTemplate;
        private readonly Transform _container;

        public TailSpawner(Tail tailTemplate, Transform container)
        {
            _tailTemplate = tailTemplate;
            _container = container;
        }

        public event Action<Tail> Spawned;

        public Tail Spawn(Snake snake)
        {
            Tail tail = GameObject.Instantiate(_tailTemplate, _container);
            tail.Init(snake.TailPoints, snake.ThicknessAndLengthUpdater, snake);
            return tail;
        }
    }
}
