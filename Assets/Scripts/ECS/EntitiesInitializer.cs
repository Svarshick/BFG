using System;
using System.Collections.Generic;
using Configs;
using Core.Query;
using DataBinding;
using ECS.GW;
using ECS.Player;
using ObservableCollections;
using Scellecs.Morpeh;
using VContainer.Unity;
using Location = Configs.Location;

namespace ECS
{
    public class EntitiesInitializer : IInitializable
    {
        private readonly World _world;
        private readonly QBuffer _qBuffer;
        private readonly FrontConfigSet _frontConfigSet;
        private readonly GameInitConfig _gameInitConfig;

        public EntitiesInitializer(
            World world, 
            QBuffer qBuffer,
            FrontConfigSet frontConfigSet,
            GameInitConfig gameInitConfig
            )
        {
            _world = world;
            _qBuffer = qBuffer;
            _frontConfigSet = frontConfigSet;
            _gameInitConfig = gameInitConfig;
        }

        //todo I don't like it
        private ObservableHashSet<Tag> _playerTags;

        public void Initialize()
        {
            InitQQueue();
            InitPlayer();
            InitGameWorld();
            _world.Commit();
        }
        
        private void InitQQueue()
        {
            var qQueue = _world.CreateEntity();
            var qQueueStash = _world.GetStash<QQueue>();
            qQueueStash.Add(qQueue, new QQueue { buffer = _qBuffer });
        }
        
        private void InitPlayer()
        {
            var taggedStash = _world.GetStash<Tagged>();
            var player =  _world.CreateEntity();
            var tagsSet = new HashSet<Tag>();
            foreach (var tag in _gameInitConfig.playerTags)
            {
                tagsSet.Add(tag);
            }

            ref var playerTags = ref taggedStash.Add(player);
            playerTags.value = new(tagsSet);
            _playerTags = playerTags.value;
        }
        
        private void InitGameWorld()
        {
            var gameWorld = _world.CreateEntity();
            var gameWorldStash = _world.GetStash<GameWorld>();
            ref var gameWorldComp = ref gameWorldStash.Add(gameWorld);
            gameWorldComp.day = 0;
            gameWorldComp.view = new(new GameWorldDataView {Day = 1});
            gameWorldComp.frontsView = new();
            gameWorldComp.reverseFrontMapping = new();
            gameWorldComp.currentFront = new();

            //todo what to do with naming Location as enum and Location as component...
            var locationStash = _world.GetStash<GW.Location>();
            var locations = new Dictionary<Location, Entity>();
            foreach (Location location in Enum.GetValues(typeof(Location)))
            {
                var locationEntity = _world.CreateEntity();
                locations[location] = locationEntity;
                locationStash.Add(locationEntity, new GW.Location { value = location, fronts = new() });
            }

            var frontStash = _world.GetStash<Front>();
            var activeStash = _world.GetStash<Active>();
            foreach (var frontConfig in _frontConfigSet.value)
            {
                var front = _world.CreateEntity();
                ref var frontComp = ref frontStash.Add(front);
                frontComp.config = frontConfig;
                var frontView = FrontUtils.AddFrontView(ref frontComp, ref gameWorldComp, ref front);
                if (frontConfig.awakeningDay == 0 &&
                    FrontUtils.RequirementsMet(_playerTags, frontConfig.requirements))
                {
                    activeStash.Add(front);
                    //todo Getting stash...
                    var locationComp = locationStash.Get(locations[frontConfig.location]);
                    locationComp.fronts.Add(frontView);
                }
            }
        }
    }
}