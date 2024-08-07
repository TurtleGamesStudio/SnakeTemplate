namespace TurtleGamesStudio.SnakeGame.Model
{
    public class SnakeClearInput : ISnakeInput
    {
        public float Angle { get; set; }
        public bool IsAccelerationPressed { get; set; }
    }
}
