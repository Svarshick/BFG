using DataBinding;
using R3;

namespace UI
{
    public class FrontInfoViewModel : ViewModel
    {
        private readonly DataViewStorage _dataViewStorage;
        public readonly GameLogicMediator GameLogicMediator;
        
        public ReactiveProperty<FrontDataView> FrontDataView { get; private set; }

        public FrontInfoViewModel(
            DataViewStorage dataViewStorage,
            GameLogicMediator gameLogicMediator,
            ReactiveProperty<FrontDataView> frontDataView
        )
        {
            _dataViewStorage = dataViewStorage;
            GameLogicMediator = gameLogicMediator;
            FrontDataView = frontDataView;
        }
    }
}