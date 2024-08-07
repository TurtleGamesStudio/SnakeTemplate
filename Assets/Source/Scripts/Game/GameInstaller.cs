using System;
using Reflex.Core;
using TurtleGamesStudio.SnakeGame.Model;
using TurtleGamesStudio.SnakeGame.Model.BoardComponents;
using UnityEngine;

namespace TurtleGamesStudio
{
    public partial class GameInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private Platform _platform;
        [SerializeField] private ViewSurface _viewSurface = ViewSurface.XY;

        [Header("SceneObject")]
        [SerializeField] private GameCameras _gameCameras;
        [SerializeField] private GameMainUi _gameMainUi;

        [Header("ScriptableObjects")]
        [SerializeField] private Models _models;
        [SerializeField] private RenderOrder _order;
        [SerializeField] private SeparationCalculator _separationCalculator;
        [SerializeField] private FoodSettings _foodSettings;

        [Header("Templates")]
        [SerializeField] private Food _food;
        [SerializeField] private Wall _wall;
        [SerializeField] private Floor _floor;
        [SerializeField] private Snake _snake;
        [SerializeField] private Tail _tail;
        [SerializeField] private Cell _cell;
        [SerializeField] private FoodController _foodController;

        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddSingleton(_platform);

            containerBuilder.AddSingleton(_gameCameras);
            containerBuilder.AddSingleton(_gameMainUi);

            containerBuilder.AddSingleton(_models);
            containerBuilder.AddSingleton(_models.WallView);
            containerBuilder.AddSingleton(_models.FloorView);
            containerBuilder.AddSingleton(_models.SnakeView);
            containerBuilder.AddSingleton(_models.TailView);
            containerBuilder.AddSingleton(_models.FoodView);
            containerBuilder.AddSingleton(_order);
            _order.Init();
            containerBuilder.AddSingleton(_separationCalculator);
            _separationCalculator.Init();
            containerBuilder.AddSingleton(_foodSettings);

            containerBuilder.AddSingleton(_food);
            containerBuilder.AddSingleton(_wall);
            containerBuilder.AddSingleton(_floor);
            containerBuilder.AddSingleton(_snake);
            containerBuilder.AddSingleton(_tail);
            containerBuilder.AddSingleton(_cell);
            containerBuilder.AddSingleton(_foodController);

            containerBuilder.AddSingleton(typeof(FoodSpawner));
            containerBuilder.AddSingleton(typeof(WallSpawner));
            containerBuilder.AddSingleton(typeof(WallViewSpawner));

            IPositionConverter viewPositionConverter = GetPositionConverter(_viewSurface);
            containerBuilder.AddSingleton(viewPositionConverter, typeof(IPositionConverter));
        }

        private IPositionConverter GetPositionConverter(ViewSurface viewSurface)
        {
            switch (viewSurface)
            {
                case ViewSurface.XY:
                    return new PositionConverterXY();
                case ViewSurface.XZ:
                    return new PositionConverterXZ();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
