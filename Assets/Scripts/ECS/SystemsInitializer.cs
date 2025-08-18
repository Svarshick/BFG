using System;
using System.Collections.Generic;
using Configs;
using Scellecs.Morpeh;
using VContainer.Unity;

namespace ECS
{
    public class SystemsInitializer : IInitializable, IDisposable
    {
        private readonly World _world;
        private readonly IEnumerable<ISystem> _systems;
        private readonly FrontConfigSet _frontConfigSet;

        public SystemsInitializer(
            World world, 
            IEnumerable<ISystem> systems,
            FrontConfigSet frontConfigSet
            )
        {
            _world = world;
            _systems = systems;
            _frontConfigSet = frontConfigSet;
        }

        public void Initialize()
        {
            var group = _world.CreateSystemsGroup();
            foreach (var system in _systems)
            {
                group.AddSystem(system);
            }
            _world.AddSystemsGroup(0, group);
        }
        
        public void Dispose()
        {
            _world?.Dispose();
        }
    }
}