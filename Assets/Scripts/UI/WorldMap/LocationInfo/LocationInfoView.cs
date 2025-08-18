using R3;
using ObservableCollections;
using DataBinding;
using UnityEngine.UIElements;
using VContainer;

namespace UI
{
    public class LocationInfoView : View
    {
        private readonly LocationInfoViewModel _viewModel;
        private readonly FrontInfoFactory _frontInfoFactory;
        private readonly UIEventAggregator _uiEventAggregator;
        
        private Label _locationInfoLabel;
        private VisualElement _frontInfoList;
        private ISynchronizedView<ReactiveProperty<FrontDataView>, FrontInfoView> _frontInfoListView;
        
        public LocationInfoView(
            LocationInfoViewModel viewModel,
            IObjectResolver container,
            UIEventAggregator uiEventAggregator,
            VisualElement root
        ) : base(root, true)
        {
            _viewModel = viewModel;
            _frontInfoFactory = container.Resolve<FrontInfoFactory>();
            _uiEventAggregator = uiEventAggregator;
        }

        protected override void SetVisualElements()
        {
            _locationInfoLabel = Root.Q<Label>("locationInfoLabel");
            _frontInfoList = Root.Q<VisualElement>("frontInfoList");
        }

        protected override void BindViewData()
        {
            _frontInfoListView = _viewModel.FrontDataViewList.CreateView(f => _frontInfoFactory.Create(f, _frontInfoList));
            _frontInfoListView.ObserveRemove().Subscribe(f => f.Value.View.Dispose());
            //todo make it work
            _frontInfoListView.ObserveCountChanged().Subscribe(n =>
            {
                if (n > 0)
                    _locationInfoLabel.text = "Есть работа";
                else
                    _locationInfoLabel.text = "Работы нет";
            });
        }

        protected override void RegisterInputCallbacks()
        {
            Root.RegisterCallback<MouseLeaveEvent>(e => Hide());
        }
        
        public override void Dispose()
        {
            _frontInfoListView.Dispose();
            base.Dispose();
        }
    }
}