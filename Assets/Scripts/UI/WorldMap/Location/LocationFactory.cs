using Configs;
using Core;
using DataBinding;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;

namespace UI
{
    public class LocationFactory
    {
        private readonly IObjectResolver _objectResolver;
        private readonly UIEventAggregator _uiEventAggregator;
        private readonly GameLogicMediator _gameLogicMediator;

        public LocationFactory(
            IObjectResolver objectResolver,
            UIEventAggregator uiEventAggregator,
            GameLogicMediator gameLogicMediator,
            [Key(UIType.WorldMap)] VisualElement root
            )
        {
            _objectResolver = objectResolver;
            _uiEventAggregator = uiEventAggregator;
            _gameLogicMediator = gameLogicMediator;
        }

        public LocationView Create(Location location, VisualElement root)
        {
            var locationViewModel = new LocationViewModel(_gameLogicMediator, location);
            var locationView = new LocationView(
                locationViewModel,
                _objectResolver,
                _uiEventAggregator,
                root
            );
            locationView.Start();
            return locationView;
        }
    }
}