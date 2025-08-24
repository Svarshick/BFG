using UI;
using UI.MainMenu;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using VContainer;
using VContainer.Unity;

namespace Core
{
    public class MainMenuLifeTimeScope : LifetimeScope
    {
        [SerializeField] private UIDocument mainMenu;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(mainMenu.rootVisualElement).Keyed(UIType.MainMenu);
            builder.Register<MainMenuViewModel>(Lifetime.Scoped);
            builder.RegisterEntryPoint<MainMenuView>(Lifetime.Scoped);
        }
    }
}