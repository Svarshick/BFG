using System;
using Core;
using DataBinding;
using R3;
using UI.ControlBar;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;

namespace UI.Front
{
    public class FrontView : View
    {
        private readonly FrontViewModel _viewModel;
        private readonly UIEventAggregator _uiEventAggregator;
        private readonly ControlBarFactory _controlBarFactory;

        private Label _frontLabel;
        private VisualElement _choiceList;
        private IDisposable _subscription;
        private VisualElement _controlBar;
        private ControlBarView _controlBarView;
        
        private ReactiveProperty<ReactiveProperty<FrontDataView>> _currentFront;

        public FrontView(
            FrontViewModel viewModel,
            IObjectResolver objectResolver,
            UIEventAggregator uiEventAggregator,
            [Key(UIType.Front)] VisualElement root
            ) : base(root, true)
        {
            _viewModel = viewModel;
            _uiEventAggregator = uiEventAggregator;
            _controlBarFactory = objectResolver.Resolve<ControlBarFactory>();
        }

        protected override void SetVisualElements()
        {
            _frontLabel = Root.Q<Label>("frontLabel");
            _choiceList = Root.Q<VisualElement>("frontChoiceList");
            _controlBar = Root.Q<VisualElement>("controlBar");
            _controlBarView = _controlBarFactory.Create(_controlBar);
        }

        protected override void BindViewData()
        {
            _currentFront = _viewModel.CurrentFrontDataView;
            _subscription = _currentFront.Subscribe(RefreshFront);
        }

        private void RefreshFront(ReactiveProperty<FrontDataView> frontObserver)
        {
            var frontView = frontObserver.Value;
            if (frontView == null)
                return;
            _frontLabel.text = frontView.Stage.Text;
            _choiceList.Clear();
            foreach (var choice in frontView.Stage.Choices)
            {
                var button = new Button();
                button.text = choice.Text;
                button.RegisterCallback<ClickEvent>(evt =>
                {
                    _viewModel.GameLogicMediator.MakeChoice(frontObserver, choice);
                    _viewModel.GameLogicMediator.EndDay();
                    _uiEventAggregator.EndFront();
                });
                _choiceList.Add(button);
            }
        }

        protected override void RegisterInputCallbacks()
        {
            _uiEventAggregator.StartFront += Show;
            _uiEventAggregator.EndFront += Hide;
        }

        public override void Dispose()
        {
            _subscription?.Dispose();
        }
    }
}