using Configs;
using DataBinding;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;

namespace UI
{
    public class LocationView : View
    {
        private readonly LocationViewModel _viewModel;
        private readonly IObjectResolver _objectResolver;
        private readonly LocationInfoFactory _locationInfoFactory;
        private readonly UIEventAggregator _uiEventAggregator;

        private Label _locationLabel;
        private LocationInfoView _locationInfo;
        
        public LocationView(
            LocationViewModel viewModel,
            IObjectResolver objectResolver,
            UIEventAggregator uiEventAggregator,
            VisualElement root
            ) : base(root)
        {
            _viewModel = viewModel;
            _objectResolver = objectResolver;
            _locationInfoFactory = _objectResolver.Resolve<LocationInfoFactory>();
            _uiEventAggregator = uiEventAggregator;
        }

        protected override void SetVisualElements()
        {
            _locationLabel = Root.Q<Label>("locationLabel");
            _locationLabel.text = _viewModel.Location.ToString();
            _locationInfo = _locationInfoFactory.Create(_viewModel.Location, Root.panel.visualTree);
        }

        protected override void RegisterInputCallbacks()
        {
            _locationLabel.RegisterCallback<MouseEnterEvent>(ShowLocationInfo);
        }

        private void ShowLocationInfo(MouseEnterEvent evt)
        {
            var offset = new Vector2(-10, -10); 
            _locationInfo.Root.style.translate = evt.mousePosition + offset;
            _locationInfo.Show();
        }
    }
}