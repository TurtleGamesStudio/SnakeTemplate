namespace TurtleGamesStudio.SnakeGame.Model
{
    public interface ISnakeInput
    {
        public float Angle { get; }
        public bool IsAccelerationPressed { get; }
    }
}
