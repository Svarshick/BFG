using DataBinding;
using R3;

namespace UI.Front
{
    public class FrontViewModel : ViewModel
    {
        private readonly DataViewStorage _dataViewStorage;
        public readonly GameLogicMediator GameLogicMediator;

        public ReactiveProperty<ReactiveProperty<FrontDataView>> CurrentFrontDataView { get; private set; }

        public FrontViewModel(
            DataViewStorage dataViewStorage,
            GameLogicMediator gameLogicMediator
        )
        {
            _dataViewStorage = dataViewStorage;
            GameLogicMediator = gameLogicMediator;
            CurrentFrontDataView = _dataViewStorage.CurrentFront;
        }
    }
}