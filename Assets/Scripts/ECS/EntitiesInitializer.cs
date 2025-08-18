using System;
using System.Collections.Generic;
using Configs;
using ECS.GameEntity;
using ECS.GameWorld;
using Scellecs.Morpeh;
using UnityEditor.U2D.Aseprite;
using VContainer.Unity;
using DataViewStorage = ECS.DataView.DataViewStorage;

namespace ECS
{
    public class EntitiesInitializer : IInitializable
    {
        private readonly World _world;
        private readonly FrontConfigSet _frontConfigSet;
        private readonly GameWorldInitConfig _gameWorldInitConfig;
        private readonly DataBinding.DataViewStorage _dataViewStorage;

        public EntitiesInitializer(
            World world, 
            FrontConfigSet frontConfigSet,
            GameWorldInitConfig gameWorldInitConfig,
            DataBinding.DataViewStorage dataViewStorage
            )
        {
            _world = world;
            _frontConfigSet = frontConfigSet;
            _gameWorldInitConfig = gameWorldInitConfig;
            _dataViewStorage = dataViewStorage;
        }

        private Dictionary<Tag, bool> dirtyHack;
        
        public void Initialize()
        {
            InitQQueue();
            InitDataViewStorage();
            InitPlayer();
            InitGameWorld();
            InitFronts();
            InitFrontReverseView();
            _world.Commit();
        }

        private void InitQQueue()
        {
            var qQueue = _world.CreateEntity();
            var qQueueStash = _world.GetStash<QQueue>();
            qQueueStash.Add(qQueue);
        }
        
        private void InitFronts()
        {
            var frontStash = _world.GetStash<Front>();
            var activeStash = _world.GetStash<Active>();
            foreach (var frontConfig in _frontConfigSet.fronts)
            {
                var front = _world.CreateEntity();
                var frontComp = new Front {config = frontConfig};
                frontStash.Add(front, frontComp);
                if (frontConfig.day == 0 && RequirementsMet(frontComp, dirtyHack))
                    activeStash.Add(front);
            }
        }
        
        public static bool RequirementsMet(Front frontComp, Dictionary<Tag, bool> dirtyTags)
        {
            foreach (var pare in frontComp.config.requirements)
            {
                if (dirtyTags[pare.Key] != pare.Value)
                    return false;
            }
            return true;
        }

        private void InitFrontReverseView()
        {
            var frontReverseView = _world.CreateEntity();
            var frontReverseViewStash = _world.GetStash<FrontReverseView>();
            frontReverseViewStash.Add(frontReverseView, new FrontReverseView {value = new()});
        }

        private void InitDataViewStorage()
        {
            var dataViewStorage = _world.CreateEntity();
            var dataViewStorageStash = _world.GetStash<DataViewStorage>();
            dataViewStorageStash.Add(dataViewStorage, new DataViewStorage {value = _dataViewStorage});
        }

        private void InitPlayer()
        {
            var player = _world.CreateEntity();
            var taggedStash = _world.GetStash<Tagged>();
            var tagsDictionary = new Dictionary<Tag, bool>();
            var tags = Enum.GetValues(typeof(Tag));
            foreach (Tag tag in tags)
            {
                if (_gameWorldInitConfig.playerTags.TryGetValue(tag, out var value))
                    tagsDictionary[tag] = value;
                else
                    tagsDictionary[tag] = false;
            }
            taggedStash.Add(player, new Tagged { value = new(tagsDictionary) });
            dirtyHack = tagsDictionary;
        }

        private void InitGameWorld()
        {
            var gameWorld = _world.CreateEntity();
            var gameWorldStash = _world.GetStash<GameWorld.GameWorld>();
            gameWorldStash.Add(gameWorld, new GameWorld.GameWorld {day = 0});
        }
    }
}