using System;
using DataBinding;
using R3;
using TriInspector;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class FrontInfoView : View
    {
        private readonly FrontInfoViewModel _viewModel;
        private readonly UIEventAggregator _uiEventAggregator;

        private Button _frontInfoGoButton;
        private IDisposable _subscription;

        public FrontInfoView(
            FrontInfoViewModel viewModel,
            UIEventAggregator uiEventAggregator,
            VisualElement root
        ) : base(root)
        {
            _viewModel = viewModel;
            _uiEventAggregator = uiEventAggregator;
        }

        protected override void SetVisualElements()
        {
            _frontInfoGoButton = Root.Q<Button>("frontInfoGoButton");
        }

        protected override void BindViewData()
        {
            _subscription = _viewModel.FrontDataView.Subscribe(f => _frontInfoGoButton.text = f.Stage.Text);
        }

        protected override void RegisterInputCallbacks()
        {
            _frontInfoGoButton.RegisterCallback<ClickEvent>(evt =>
            {
                _viewModel.GameLogicMediator.StartFront(_viewModel.FrontDataView);
                _uiEventAggregator.StartFront();
            });
        }

        public override void Dispose()
        {
            _subscription?.Dispose();
            base.Dispose();
        }
    }
}