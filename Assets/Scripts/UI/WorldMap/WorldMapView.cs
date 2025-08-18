using System;
using System.Collections.Generic;
using System.Linq;
using Configs;
using R3;
using Core;
using DataBinding;
using ObservableCollections;
using UI.ControlBar;
using UnityEngine.UIElements;
using VContainer;

namespace UI
{
    public class WorldMapView : View
    {
        private readonly WorldMapViewModel _viewModel;
        private readonly UIEventAggregator _uiEventAggregator;
        private readonly LocationFactory _locationFactory;
        private readonly ControlBarFactory _controlBarFactory;
        
        private List<LocationView> _locationViewList;
        private VisualElement _controlBar;
        private ControlBarView _controlBarView;
        
        public WorldMapView(
            WorldMapViewModel viewModel,
            IObjectResolver objectResolver,
            UIEventAggregator uiEventAggregator,
            [Key(UIType.WorldMap)] VisualElement root
            ) : base(root)
        {
            _viewModel = viewModel;
            _uiEventAggregator = uiEventAggregator;
            _locationFactory = objectResolver.Resolve<LocationFactory>();
            _controlBarFactory = objectResolver.Resolve<ControlBarFactory>();
        }

        protected override void SetVisualElements()
        {
            var locations = Enum.GetValues(typeof(Location));
            _locationViewList = new List<LocationView>();
            foreach (var location in locations)
            {
                var locationRoot = Root.Q<VisualElement>("location" + (int)location);
                _locationViewList.Add(_locationFactory.Create((Location)location, locationRoot));
            }
            _controlBar = Root.Q<VisualElement>("controlBar");
            _controlBarView = _controlBarFactory.Create(_controlBar);
        }

        protected override void RegisterInputCallbacks()
        {
            _uiEventAggregator.StartFront += Hide;
            _uiEventAggregator.EndFront += Show;
        }

    }
}