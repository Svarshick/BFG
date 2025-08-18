using DataBinding;
using R3;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class FrontInfoFactory
    {
        private readonly UIEventAggregator _uiEventAggregator;
        private readonly DataViewStorage _dataViewStorage;
        private readonly GameLogicMediator _gameLogicMediator;
        
        private readonly VisualTreeAsset _frontInfoAsset;

        public FrontInfoFactory(
            UIEventAggregator uiEventAggregator,
            DataViewStorage dataViewStorage,
            GameLogicMediator gameLogicMediator
        )
        {
            _uiEventAggregator = uiEventAggregator;
            _dataViewStorage = dataViewStorage;
            _gameLogicMediator = gameLogicMediator;
            _frontInfoAsset = Resources.Load<VisualTreeAsset>("front-info");
        }
        
        public FrontInfoView Create(ReactiveProperty<FrontDataView> frontDataView, VisualElement root)
        {
            var frontInfoRoot = _frontInfoAsset.Instantiate();
            root.Add(frontInfoRoot);
            var frontInfoViewModel = new FrontInfoViewModel(_dataViewStorage, _gameLogicMediator, frontDataView);
            var frontInfoView = new FrontInfoView(frontInfoViewModel, _uiEventAggregator, frontInfoRoot);
            frontInfoView.Start();
            return frontInfoView;
        }
    }
}