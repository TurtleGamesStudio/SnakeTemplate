using UnityEngine.Events;
using UnityEngine.UI;

namespace TurtleGamesStudio.Extensions
{
    public static class ButtonExtensions
    {
        public static void Add(this Button button, UnityAction action)
            => button.onClick.AddListener(action);

        public static void Remove(this Button button, UnityAction action)
            => button.onClick.RemoveListener(action);
    }
}
