using Cinemachine;
using UnityEngine;

namespace TurtleGamesStudio
{
    public class GameCameras : MonoBehaviour
    {
        [field: SerializeField] public CinemachineVirtualCamera SnakeCamera { get; private set; }
        [field: SerializeField] public Camera MainCamera { get; private set; }
    }
}
