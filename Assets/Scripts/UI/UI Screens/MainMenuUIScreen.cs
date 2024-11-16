using CORE.Gameplay;
using Patterns.ServiceLocator;
using UnityEngine.UIElements;

namespace UI
{
    public class MainMenuUIScreen : UIScreen
    {
        private Button _playButton;
        private GameManager _gameManager;
        
        public MainMenuUIScreen(VisualElement root) : base(root)
        {
            _playButton = _rootElement.Q<Button>("PLAY-BUTTON");
            _eventRegister.RegisterCallback<ClickEvent>(_playButton, () =>
            {
                UIEventDocker.OnGameplayUIShown.Invoke();
                StartGame();
            });
        }

        private void StartGame()
        {
            (_gameManager ?? ServiceLocator.GetService<GameManager>()).StartGame();
        }
    }
}