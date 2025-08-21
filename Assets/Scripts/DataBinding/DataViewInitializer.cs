using ECS.GW;
using ECS.Player;
using Scellecs.Morpeh;
using VContainer.Unity;

namespace DataBinding
{
    public class DataViewInitializer : IInitializable
    {
        private DataViewStorage _dataViewStorage;
        private World _world;

        DataViewInitializer(
            DataViewStorage dataViewStorage,
            World world
            )
        {
            _dataViewStorage = dataViewStorage;
            _world = world;
        }
        
        public void Initialize()
        {
            var gameWorldStash = _world.GetStash<GameWorld>();
            var gameWorld = _world.Filter.With<GameWorld>().Build().First();
            ref var gameWorldComp = ref gameWorldStash.Get(gameWorld);
            _dataViewStorage.WorldFronts = gameWorldComp.frontsView;
            _dataViewStorage.CurrentFront = gameWorldComp.currentFront;
            _dataViewStorage.GameWorld = gameWorldComp.view;
            
            var locationStash = _world.GetStash<Location>();
            var locationFilter = _world.Filter.With<Location>().Build();
            _dataViewStorage.LocationFronts = new();
            foreach (var location in locationFilter)
            {
                ref var locationComp = ref locationStash.Get(location);
                _dataViewStorage.LocationFronts[locationComp.value] = locationComp.fronts;
            }

            var taggedStash = _world.GetStash<Tagged>();
            var player = _world.Filter.With<Tagged>().Build().First();
            ref var playerTags = ref taggedStash.Get(player);
            _dataViewStorage.PlayerTags = playerTags.value;
        }
    }
}