using Configs;
using DataBinding;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;

namespace UI
{
    public class LocationInfoFactory
    {
        private readonly IObjectResolver _objectResolver;
        private readonly UIEventAggregator _uiEventAggregator;
        private readonly DataViewStorage _dataViewStorage;
        private readonly GameLogicMediator _gameLogicMediator;
        
        private readonly VisualTreeAsset _locationInfoAsset;

        public LocationInfoFactory(
            IObjectResolver  objectResolver,
            UIEventAggregator uiEventAggregator,
            DataViewStorage dataViewStorage,
            GameLogicMediator gameLogicMediator
            )
        {
            _objectResolver = objectResolver;
            _uiEventAggregator = uiEventAggregator;
            _dataViewStorage = dataViewStorage;
            _gameLogicMediator = gameLogicMediator;
            _locationInfoAsset = Resources.Load<VisualTreeAsset>("location-info");
        }

        public LocationInfoView Create(Location location, VisualElement root)
        {
            var locationInfoRoot = _locationInfoAsset.Instantiate();
            root.Add(locationInfoRoot);
            var locationInfoViewModel = new LocationInfoViewModel(location, _dataViewStorage, _gameLogicMediator);
            var locationInfoView = new LocationInfoView(locationInfoViewModel, _objectResolver, _uiEventAggregator, locationInfoRoot);
            locationInfoView.Start();
            return locationInfoView;
        }
    }
}