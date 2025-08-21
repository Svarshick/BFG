using System;
using System.Collections.Generic;
using Configs;
using ObservableCollections;
using R3;

namespace DataBinding
{
    public class DataViewStorage
    {
        public HashSet<ReactiveProperty<FrontDataView>> WorldFronts { get; set; }
        public Dictionary<Location, ObservableList<ReactiveProperty<FrontDataView>>> LocationFronts { get; set; }
        public ReactiveProperty<ReactiveProperty<FrontDataView>> CurrentFront { get; set; }
        public ReactiveProperty<GameWorldDataView> GameWorld { get; set; }
        public ObservableHashSet<Tag> PlayerTags { get; set; }
    }
}