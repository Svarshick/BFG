using System.Collections.Generic;
using Core.Query;
using DataBinding;
using ECS.Player;

namespace ECS.GW
{
    using Scellecs.Morpeh;

    public sealed class GameWoldCycleSystem : ISystem
    {
        public World World { get; set; }

        private Entity _qQueue;
        private Stash<QQueue> _qQueueStash;
        
        private Entity _gameWorld;
        private Stash<GameWorld> _gameWorldStash;
        
        private Stash<Location> _locationStash;
        private Filter _locationFilter;
        private Dictionary<Configs.Location, Entity> _locations;

        private Stash<Front> _frontStash;
        private Stash<Active> _activeStash;
        private Filter _inactiveFrontFilter;
        private Filter _activeFrontFilter;
 
        private Entity _player;
        private Stash<Tagged> _taggedStash;
       
        public void OnAwake()
        {
            _qQueue = World.Filter.With<QQueue>().Build().First();
            _qQueueStash = World.GetStash<QQueue>();
            
            _gameWorld = World.Filter.With<GameWorld>().Build().First();
            _gameWorldStash = World.GetStash<GameWorld>();
            
            _locationStash = World.GetStash<Location>();
            _locationFilter = World.Filter.With<Location>().Build();
            //todo I don't like it
            _locations = new();
            
            _player = World.Filter.With<Tagged>().Build().First();
            _taggedStash = World.GetStash<Tagged>();
            
            _frontStash = World.GetStash<Front>();
            _activeStash = World.GetStash<Active>();
            _inactiveFrontFilter = World.Filter.With<Front>().Without<Active>().Build();
            _activeFrontFilter = World.Filter.With<Front>().With<Active>().Build();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var location in _locationFilter)
            {
                ref var locationComp = ref _locationStash.Get(location);
                _locations[locationComp.value] = location;
            }
            ref var qQueueComp = ref _qQueueStash.Get(_qQueue);
            foreach (var q in qQueueComp.value)
            {
                if (q is StartFrontQ startFrontQ)
                    StartFront(startFrontQ);
                if (q is EndDayQ endDayQ)
                    EndDay(endDayQ);
            }
        }

        private void StartFront(StartFrontQ q)
        {
            ref var gameWorldComp = ref _gameWorldStash.Get(_gameWorld);
            gameWorldComp.currentFront.Value = q.front;
        }

        private void EndDay(EndDayQ q)
        {
            ref var gameWorldComp = ref _gameWorldStash.Get(_gameWorld);
            ref var playerTags = ref _taggedStash.Get(_player);
            //todo I don't want parameters here. At least it should be inline instead of calling
            ShiftFronts(ref gameWorldComp, ref playerTags);
            AwakeFronts(ref gameWorldComp, ref playerTags);
            gameWorldComp.day += 1;
            gameWorldComp.view.Value = new GameWorldDataView { Day = gameWorldComp.day + 1 };
            return;

            void ShiftFronts(ref GameWorld gameWorldComp, ref Tagged playerTags)
            {
                foreach (var front in _activeFrontFilter)
                {
                    ref var frontComp = ref _frontStash.Get(front);
                    var nextStageNumber = gameWorldComp.day + 1 - frontComp.config.awakeningDay;
                    if (nextStageNumber < frontComp.config.stages.Count)
                    {
                        frontComp.view.Value = FrontUtils.CreateFrontView(frontComp, nextStageNumber);
                    }
                    else
                    {
                        FrontUtils.Affect(playerTags.value, frontComp.config.fiascoEffect);
                        _activeStash.Remove(front);
                        var locationComp = _locationStash.Get(_locations[frontComp.config.location]);
                        locationComp.fronts.Remove(frontComp.view);
                    }
                }
            }
            
            void AwakeFronts(ref GameWorld gameWorldComp, ref Tagged playerTags)
            {
                foreach (var front in _inactiveFrontFilter)
                {
                    ref var frontComp = ref _frontStash.Get(front);
                    if (frontComp.config.awakeningDay == gameWorldComp.day + 1 &&
                        FrontUtils.RequirementsMet(playerTags.value, frontComp.config.requirements))
                    {
                        _activeStash.Add(front);
                        ref var locationComp = ref _locationStash.Get(_locations[frontComp.config.location]);
                        locationComp.fronts.Add(frontComp.view);
                    }
                }
            }
        }
        
        public void Dispose()
        {
        }
    }
}