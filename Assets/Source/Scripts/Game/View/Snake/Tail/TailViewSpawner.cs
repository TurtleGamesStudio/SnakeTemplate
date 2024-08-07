using Reflex.Core;
using Reflex.Injectors;
using TurtleGamesStudio.SnakeGame.Model;
using UnityEngine;

namespace TurtleGamesStudio
{
    public class TailViewSpawner
    {
        private readonly TailView _viewTemplate;
        private readonly IPositionConverter _positionConverter;
        private readonly int _order;
        private readonly Container _diContainer;
        private readonly Transform _container;

        public TailViewSpawner(TailView viewTemplate, IPositionConverter positionConverter, int order,
             Container diContainer, Transform container)
        {
            _positionConverter = positionConverter;
            _viewTemplate = viewTemplate;
            _diContainer = diContainer;
            _container = container;
            _order = order;
        }

        public TailView Spawn(Tail tail)
        {
            TailView view = GameObject.Instantiate(_viewTemplate, _container);
            AttributeInjector.Inject(view, _diContainer);
            view.Init(tail, _positionConverter, _order);
            return view;
        }
    }
}
