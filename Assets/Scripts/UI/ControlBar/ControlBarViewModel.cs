using Configs;
using DataBinding;
using ObservableCollections;
using R3;

namespace UI.ControlBar
{
    public class ControlBarViewModel : ViewModel
    {
        private readonly DataViewStorage _dataViewStorage;
        public readonly GameLogicMediator GameLogicMediator;

        public ObservableDictionary<Tag, bool> PlayerTags => _dataViewStorage.PlayerTags;
        public ReactiveProperty<GameWorldDataView> GameWorld => _dataViewStorage.GameWorld;

        public ControlBarViewModel(
            DataViewStorage dataViewStorage,
            GameLogicMediator gameLogicMediator
            )
        {
            _dataViewStorage = dataViewStorage;
            GameLogicMediator = gameLogicMediator;
        }
}
}