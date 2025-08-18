using System.Linq;
using Core.Query;
using ECS.DataView;
using ECS.GameEntity;
using Scellecs.Morpeh;
using UnityEngine;

namespace ECS.GameWorld
{
    public class FrontLifetimeSystem : ISystem
    {
        public World World { get; set; }

        private Entity _qQueueEntity;
        private Stash<QQueue> _qQueueStash;
        
        private Entity _dataViewStorage;
        private Stash<DataViewStorage> _dataViewStorageStash;
        
        private Entity _gameWorld;
        private Stash<GameWorld> _gameWorldStash;

        private Entity _frontReverseView;
        private Stash<FrontReverseView> _frontReverseViewStash;
        
        private Stash<Front> _frontStash;
        private Stash<Active> _activeStash;
        
        private Entity _player;
        private Stash<Tagged> _taggedStash;

        public void OnAwake()
        {
            _qQueueEntity = World.Filter.With<QQueue>().Build().First();
            _qQueueStash = World.GetStash<QQueue>();
            
            _dataViewStorage = World.Filter.With<DataViewStorage>().Build().First();
            _dataViewStorageStash = World.GetStash<DataViewStorage>();
            
            _gameWorld = World.Filter.With<GameWorld>().Build().First();
            _gameWorldStash = World.GetStash<GameWorld>();

            _frontReverseView = World.Filter.With<FrontReverseView>().Build().First();
            _frontReverseViewStash = World.GetStash<FrontReverseView>();
            
            _frontStash = World.GetStash<Front>();
            _activeStash = World.GetStash<Active>();
            
            _player = World.Filter.With<Tagged>().Build().First();
            _taggedStash = World.GetStash<Tagged>();
        }

        public void OnUpdate(float deltaTime)
        {
            ref var qQueueComp = ref _qQueueStash.Get(_qQueueEntity);
            ref var dataViewStorageComp = ref _dataViewStorageStash.Get(_dataViewStorage);
            foreach (var q in qQueueComp.value.OfType<StartFrontQ>())
            {
                dataViewStorageComp.value.CurrentFront.Value = q.front;
            }
            ref var frontReverseViewComp = ref _frontReverseViewStash.Get(_frontReverseView);
            ref var gameWorldComp = ref  _gameWorldStash.Get(_gameWorld);
            ref var playerTags = ref _taggedStash.Get(_player);
            foreach (var q in qQueueComp.value.OfType<MakeChoiceQ>())
            {
                var front = frontReverseViewComp.value[q.front];
                var frontComp = _frontStash.Get(front);
                foreach (var effect in frontComp.config.stages[gameWorldComp.day - frontComp.config.day].choices[q.choiceNumber-1].effect)
                {
                    playerTags.value[effect.Key] = effect.Value;
                }
                _activeStash.Remove(front);
                dataViewStorageComp.value.LocationFronts[frontComp.config.location].Remove(frontComp.dataView);
            }
        }

        private void StartFront(StartFrontQ q, DataViewStorage dataViewStorageComp)
        {
            dataViewStorageComp.value.CurrentFront.Value = q.front;
        }

        public void Dispose()
        {
            
        }
    }
}