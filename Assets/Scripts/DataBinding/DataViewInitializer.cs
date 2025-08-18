using ECS;
using ECS.GameEntity;
using ECS.GameWorld;
using R3;
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
            var frontStash = _world.GetStash<Front>();
            var frontFilter = _world.Filter.With<Front>().Build();
            var activeFrontFilter = _world.Filter.With<Front>().With<Active>().Build();
            
            var frontReverseViewStash = _world.GetStash<FrontReverseView>();
            var frontReverseViewFilter = _world.Filter.With<FrontReverseView>().Build();
            var frontReverseView = frontReverseViewFilter.First();
            ref var frontReverseViewComp = ref frontReverseViewStash.Get(frontReverseView);

            foreach (var front in frontFilter)
            {
                ref var frontComp = ref frontStash.Get(front);
                var frontDataView = FrontDataView.Create(frontComp, 0);
                var reactiveView = new ReactiveProperty<FrontDataView>(frontDataView);
                frontComp.dataView = reactiveView;
                _dataViewStorage.WorldFronts.Add(reactiveView);
                frontReverseViewComp.value[reactiveView] = front;
            }
            foreach (var front in activeFrontFilter)
            {
                ref var frontComp = ref frontStash.Get(front);
                _dataViewStorage.LocationFronts[frontComp.config.location].Add(frontComp.dataView);
            }

            var gameWorld = _world.Filter.With<GameWorld>().Build().First();
            var gameWorldStash = _world.GetStash<GameWorld>();
            ref var gameWorldComp = ref gameWorldStash.Get(gameWorld);
            var gameWorldDataView = new GameWorldDataView { Day = gameWorldComp.day + 1 };
            var reactiveGameWorld = new ReactiveProperty<GameWorldDataView>(gameWorldDataView);
            gameWorldComp.dataView = reactiveGameWorld;
            _dataViewStorage.GameWorld = reactiveGameWorld;

            var player = _world.Filter.With<Tagged>().Build().First();
            var taggedStash = _world.GetStash<Tagged>();
            ref var playerTags = ref taggedStash.Get(player);
            _dataViewStorage.PlayerTags = playerTags.value;
        }
    }
}