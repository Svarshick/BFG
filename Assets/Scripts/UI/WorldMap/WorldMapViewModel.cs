using Configs;
using DataBinding;
using ObservableCollections;
using R3;

namespace UI
{
    public class WorldMapViewModel
    {
        private readonly DataViewStorage _dataViewStorage;
        public readonly GameLogicMediator GameLogicMediator;

        
        public WorldMapViewModel(
            DataViewStorage dataViewStorage,
            GameLogicMediator gameLogicMediator)
        {
            _dataViewStorage = dataViewStorage;
            GameLogicMediator = gameLogicMediator;
        }
    }
}