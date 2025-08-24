using Core;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using VContainer;

namespace UI.MainMenu
{
    public class MainMenuView : View
    {
        private Button _startGameButton;
        
        public MainMenuView(
            [Key(UIType.MainMenu)] VisualElement root
        ) : base(root)
        {
        }

        protected override void SetVisualElements()
        {
            _startGameButton = Root.Q<Button>("startGameButton");
        }

        protected override void RegisterInputCallbacks()
        {
            _startGameButton.RegisterCallback<ClickEvent>(evt=> SceneManager.LoadScene("GameScene"));
        }
    }
}