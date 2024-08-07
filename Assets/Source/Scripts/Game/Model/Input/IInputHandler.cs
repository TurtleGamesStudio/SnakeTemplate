namespace TurtleGamesStudio.SnakeGame.Model
{
    public interface IInputHandler
    {
        void SetSnake(Snake ownSnake);
        void RemoveSnake();
    }
}
