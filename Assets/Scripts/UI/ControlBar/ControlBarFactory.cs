using DataBinding;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.ControlBar
{
    public class ControlBarFactory
    {
        private readonly UIEventAggregator _uiEventAggregator;
        private readonly DataViewStorage _dataViewStorage;
        private readonly GameLogicMediator _gameLogicMediator;
        
        private readonly VisualTreeAsset _controlBarAsset;

        public ControlBarFactory(
            UIEventAggregator uiEventAggregator,
            DataViewStorage dataViewStorage,
            GameLogicMediator gameLogicMediator
        )
        {
            _uiEventAggregator = uiEventAggregator;
            _dataViewStorage = dataViewStorage;
            _gameLogicMediator = gameLogicMediator;
            _controlBarAsset = Resources.Load<VisualTreeAsset>("control-bar");
        }
        
        public ControlBarView Create(VisualElement root)
        {
            var controlBarViewModel = new ControlBarViewModel(_dataViewStorage, _gameLogicMediator);
            var controlBarView = new ControlBarView(controlBarViewModel, _uiEventAggregator, root);
            controlBarView.Start();
            return controlBarView;;
        }
    }
}