using System.Collections.Generic;
using Configs;
using DataBinding;
using ObservableCollections;
using R3;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.ControlBar
{
    public class ControlBarView : View
    {
        private readonly ControlBarViewModel _viewModel;
        private readonly UIEventAggregator _uiEventAggregator;

        private Button _skipDay;
        private Label _currentDay;
        private VisualElement _playerInfo;
        private ScrollView _playerTags;
        private ISynchronizedView<KeyValuePair<Tag, bool>, Label> _playerTagsView;

        public ControlBarView(
            ControlBarViewModel viewModel,
            UIEventAggregator uiEventAggregator,
            VisualElement root
        ) : base(root)
        {
            _viewModel =  viewModel;
            _uiEventAggregator = uiEventAggregator;
        }

        protected override void SetVisualElements()
        {
            _skipDay = Root.Q<Button>("skipDay");
            _currentDay = Root.Q<Label>("currentDay");
            _playerInfo = Root.Q<VisualElement>("playerInfo");
            _playerTags = Root.Q<ScrollView>("playerTags");
        }

        protected override void BindViewData()
        {
            _viewModel.GameWorld.Subscribe(n => _currentDay.text = "день: " + n.Day.ToString());
            //todo should be new view
            _playerTagsView = _viewModel.PlayerTags.CreateView(pare =>
            {
                if (!pare.Value) 
                    return null;
                var label = new Label();
                label.text = pare.Key.ToString();
                _playerTags.Add(label);
                return label;
            });
            _playerTagsView.ObserveReplace().Subscribe(evt =>
            {
                if (evt.OldValue.View != null)
                    _playerTags.Remove(evt.OldValue.View);
            });
            _playerTagsView.ObserveRemove().Subscribe(evt =>
            {
                if (evt.Value.View != null)
                    _playerTags.Remove(evt.Value.View);
            });
            //todo crack
            //_playerTagsView.AttachFilter(pare => pare.Value);
        }
        
        protected override void RegisterInputCallbacks()
        {
            _skipDay.RegisterCallback<ClickEvent>(evt =>
            {
                _uiEventAggregator.EndFront();
                _viewModel.GameLogicMediator.EndDay();
            });
            _playerInfo.RegisterCallback<MouseEnterEvent>(evt => _playerTags.style.display = DisplayStyle.Flex);
            _playerInfo.RegisterCallback<MouseLeaveEvent>(evt => _playerTags.style.display = DisplayStyle.None);
        }
        
        public override void Dispose()
        {
            _playerTagsView.Dispose();
            base.Dispose();
        }
    }
}