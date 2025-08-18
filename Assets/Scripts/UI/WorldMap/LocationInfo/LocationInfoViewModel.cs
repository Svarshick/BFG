using Configs;
using DataBinding;
using ObservableCollections;
using R3;

namespace UI
{
    public class LocationInfoViewModel
    {
        Location _location;
        private readonly DataViewStorage _dataViewStorage;
        public readonly GameLogicMediator GameLogicMediator;
        
        public ObservableList<ReactiveProperty<FrontDataView>> FrontDataViewList { get; private set; }
        
        public LocationInfoViewModel(
            Location location,
            DataViewStorage dataViewStorage,
            GameLogicMediator gameLogicMediator)
        {
            _location = location;
            _dataViewStorage = dataViewStorage;
            GameLogicMediator = gameLogicMediator;
            FrontDataViewList = _dataViewStorage.LocationFronts[_location];
        }
    }
}