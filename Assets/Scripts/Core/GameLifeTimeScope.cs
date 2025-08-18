using Configs;
using Core.Input;
using Core.Query;
using DataBinding;
using ECS;
using ECS.GameWorld;
using Scellecs.Morpeh;
using UI;
using UI.ControlBar;
using UI.Front;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;
using VContainer.Unity;

namespace Core
{
    public enum UIType
    {
        WorldMap,
        Front
    }
    public class GameLifeTimeScope : LifetimeScope
    {
        [SerializeField] private FrontConfigSet frontConfigSet;
        [SerializeField] private GameWorldInitConfig worldInitConfig;
        [SerializeField] private UIDocument worldMap;
        [SerializeField] private UIDocument front;
        [SerializeField] private GameLogicMediator gameLogicMediator;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(frontConfigSet);
            builder.RegisterInstance(worldInitConfig);
            
            //Reactivity
            builder.Register<QBuffer>(Lifetime.Singleton);
            builder.RegisterEntryPoint<InputRecorder>();
            builder.Register<DataViewStorage>(Lifetime.Singleton);
            builder.RegisterComponent(gameLogicMediator);
            builder.Register<UIEventAggregator>(Lifetime.Singleton);
            
            //ECS
            builder.RegisterInstance(World.Default);
            builder.RegisterEntryPoint<EntitiesInitializer>();
            RegisterSystems(builder);
            builder.RegisterEntryPoint<SystemsInitializer>();
           
            //UI
            builder.RegisterEntryPoint<DataViewInitializer>();
            builder.RegisterInstance(worldMap.rootVisualElement).Keyed(UIType.WorldMap);
            builder.RegisterInstance(front.rootVisualElement).Keyed(UIType.Front);
            builder.Register<FrontInfoFactory>(Lifetime.Singleton);
            builder.Register<LocationInfoFactory>(Lifetime.Singleton);
            builder.Register<LocationFactory>(Lifetime.Singleton);
            builder.Register<ControlBarFactory>(Lifetime.Singleton);
            builder.Register<WorldMapViewModel>(Lifetime.Singleton);
            builder.RegisterEntryPoint<WorldMapView>();
            builder.Register<FrontViewModel>(Lifetime.Singleton);
            builder.RegisterEntryPoint<FrontView>();
        }

        private void RegisterSystems(IContainerBuilder builder)
        {
            builder.RegisterSystem<QReadSystem>(Lifetime.Singleton);
            builder.RegisterSystem<FrontLifetimeSystem>(Lifetime.Singleton);
            builder.RegisterSystem<EndDaySystem>(Lifetime.Singleton);
            builder.RegisterSystem<QClearSystem>(Lifetime.Singleton);
        }
    }
}