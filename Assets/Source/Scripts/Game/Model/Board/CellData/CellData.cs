namespace TurtleGamesStudio.SnakeGame.Model.BoardComponents
{
    public class CellData
    {
        public bool IsFilled { get; private set; }
        public CellColliderType ColliderType { get; private set; }
        public FoodSpawnType FoodSpawnType { get; private set; }

        public CellData(bool isFilled, CellColliderType colliderType, FoodSpawnType foodSpawnType)
        {
            IsFilled = isFilled;
            ColliderType = colliderType;
            FoodSpawnType = foodSpawnType;
        }
    }
}