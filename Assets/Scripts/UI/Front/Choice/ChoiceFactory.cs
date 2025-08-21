using DataBinding;
using R3;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Front.Choice
{
    public class ChoiceFactory
    {
        private readonly UIEventAggregator _uiEventAggregator;
        private readonly DataViewStorage _dataViewStorage;
        private readonly GameLogicMediator _gameLogicMediator;

        private readonly VisualTreeAsset _choiceAsset;
        
        public ChoiceFactory(
            UIEventAggregator uiEventAggregator,
            DataViewStorage dataViewStorage,
            GameLogicMediator gameLogicMediator
        )
        {
            _uiEventAggregator = uiEventAggregator;
            _dataViewStorage = dataViewStorage;
            _gameLogicMediator = gameLogicMediator;
            _choiceAsset = Resources.Load<VisualTreeAsset>("choice");
        }
        
        public ChoiceView Create(ReactiveProperty<FrontDataView> frontDataView, StageChoice stageChoice, VisualElement root)
        {
            var choiceRoot = _choiceAsset.Instantiate();
            root.Add(choiceRoot);
            var choiceViewModel = new ChoiceViewModel(
                _dataViewStorage,
                _gameLogicMediator,
                frontDataView,
                stageChoice
            );
            var choiceView = new ChoiceView(choiceViewModel, _uiEventAggregator, choiceRoot);
            choiceView.Start();
            return choiceView;
        }
    }
}