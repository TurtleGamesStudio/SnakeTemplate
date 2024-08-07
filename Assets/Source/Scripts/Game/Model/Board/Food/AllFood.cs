using TurtleGamesStudio.ExtendedCollections;

namespace TurtleGamesStudio.SnakeGame.Model.BoardComponents
{
    public class AllFood
    {
        private readonly FoodSpawner _foodSpawner;
        private readonly ReactiveList<Food> _allFood = new();

        public IReadOnlyReactiveList<Food> All => _allFood;

        public AllFood(FoodSpawner foodSpawner)
        {
            _foodSpawner = foodSpawner;

            _foodSpawner.Spawned += OnSpawned;
        }

        private void OnSpawned(Food food)
        {
            food.Collected += OnCollected;
            _allFood.Add(food);
        }

        private void OnCollected(Food food)
        {
            food.Collected -= OnCollected;
            _allFood.Remove(food);
        }
    }
}