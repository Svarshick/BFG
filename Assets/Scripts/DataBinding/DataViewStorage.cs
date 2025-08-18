using System;
using System.Collections.Generic;
using Configs;
using ObservableCollections;
using R3;

namespace DataBinding
{
    public class DataViewStorage
    {
        //todo kill him
        private ReactiveProperty<FrontDataView> _kludgeFront;
        
        public HashSet<ReactiveProperty<FrontDataView>> WorldFronts { get; set; }
        public Dictionary<Location, ObservableList<ReactiveProperty<FrontDataView>>> LocationFronts { get; set; }
        public ReactiveProperty<ReactiveProperty<FrontDataView>> CurrentFront { get; set; }
        public ReactiveProperty<GameWorldDataView> GameWorld { get; set; }
        public ObservableDictionary<Tag, bool> PlayerTags { get; set; }

        //todo should it be initialized here?
        public DataViewStorage(FrontConfigSet frontConfigSet)
        {
            _kludgeFront = new ReactiveProperty<FrontDataView>();
            CurrentFront = new(_kludgeFront);
            WorldFronts = new();
            var locations = Enum.GetValues(typeof(Location));
            LocationFronts = new(locations.Length);
            foreach (Location location in locations)
            {
                LocationFronts[location] = new();
            }
        }
    }
}