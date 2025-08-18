using System.Linq;
using Core.Query;
using DataBinding;
using ECS.GameEntity;
using Scellecs.Morpeh;
using DataViewStorage = ECS.DataView.DataViewStorage;

namespace ECS.GameWorld
{
    public class EndDaySystem : ISystem
    {
        public World World { get; set; }

        private Entity _qQueue;
        private Stash<QQueue> _qQueueStash;

        private Entity _dataViewStorage;
        private Stash<DataViewStorage> _dataViewStorageStash;

        private Entity _gameWorld;
        private Stash<GameWorld> _gameWorldStash;
        
        private Filter _frontFilter;
        private Filter _activeFrontFilter;
        private Stash<Front> _frontStash;
        private Stash<Active> _activeStash;

        private Entity _player;
        private Stash<Tagged> _taggedStash;
        
        public void OnAwake()
        {
            _qQueue = World.Filter.With<QQueue>().Build().First();
            _qQueueStash = World.GetStash<QQueue>();

            _dataViewStorage =  World.Filter.With<DataViewStorage>().Build().First();
            _dataViewStorageStash = World.GetStash<DataViewStorage>();
            
            _gameWorld = World.Filter.With<GameWorld>().Build().First();
            _gameWorldStash = World.GetStash<GameWorld>();
            
            _frontFilter = World.Filter.With<Front>().Build();
            _activeFrontFilter = World.Filter.With<Front>().With<Active>().Build();
            _frontStash = World.GetStash<Front>();
            _activeStash = World.GetStash<Active>();
            
            _player = World.Filter.With<Tagged>().Build().First();
            _taggedStash = World.GetStash<Tagged>();
        }

        public void OnUpdate(float deltaTime)
        {
            ref var qQueueComp = ref _qQueueStash.Get(_qQueue);
            foreach (var q in qQueueComp.value.OfType<EndDayQ>())
            {
                ShiftFronts();
                AwakeFronts();
                ref var gameWorldComp = ref _gameWorldStash.Get(_gameWorld);
                gameWorldComp.day += 1;
                gameWorldComp.dataView.Value = new GameWorldDataView { Day = gameWorldComp.day + 1 };
            }
        }

        private void ShiftFronts()
        {
            ref var gameWorldComp = ref _gameWorldStash.Get(_gameWorld);
            ref var dataViewStorageComp = ref _dataViewStorageStash.Get(_dataViewStorage);
            ref var playerTags = ref _taggedStash.Get(_player);
            foreach (var front in _activeFrontFilter)
            {
                ref var frontComp = ref _frontStash.Get(front);
                int nextStageNumber = gameWorldComp.day + 1 - frontComp.config.day;
                // if not last stage, continue
                if (nextStageNumber < frontComp.config.stages.Count)
                {
                    frontComp.dataView.Value = FrontDataView.Create(frontComp, nextStageNumber);
                }
                else
                {
                    _activeStash.Remove(front);
                    dataViewStorageComp.value.LocationFronts[frontComp.config.location].Remove(frontComp.dataView);
                    foreach (var effect in frontComp.config.fiascoEffect)
                    {
                        playerTags.value[effect.Key] = effect.Value;
                    }
                }
            }
        }

        private void AwakeFronts()
        {
            ref var gameWorldComp = ref _gameWorldStash.Get(_gameWorld);
            ref var dataViewStorageComp = ref _dataViewStorageStash.Get(_dataViewStorage);
            ref var playerTags = ref _taggedStash.Get(_player);
            foreach (var front in _frontFilter)
            {
                ref var frontComp = ref _frontStash.Get(front);
                if (frontComp.config.day == gameWorldComp.day + 1 && RequirementsMet(frontComp, playerTags))
                {
                    _activeStash.Add(front);
                    dataViewStorageComp.value.LocationFronts[frontComp.config.location].Add(frontComp.dataView);
                }
            }
        }
        
        public static bool RequirementsMet(Front frontComp, Tagged tags)
        {
            foreach (var pare in frontComp.config.requirements)
            {
                if (tags.value[pare.Key] != pare.Value)
                    return false;
            }
            return true;
        }

        public void Dispose()
        {
        }
    }
}