using CORE.Gameplay;
using Patterns.ServiceLocator;
using UnityEngine.UIElements;

namespace UI
{
    public class GameOverUIScreen : UIScreen
    {
        private Button _replayButton;
        private GameManager _gameManager;

        public GameOverUIScreen(VisualElement root) : base(root)
        {
            _replayButton = _rootElement.Q<Button>("PLAY-AGAIN-BUTTON");
            _eventRegister.RegisterCallback<ClickEvent>(_replayButton, () =>
            {
                UIEventDocker.OnGameplayUIShown.Invoke();
                RestartGame();
            });
        }
        
        private void RestartGame()
        {
            (_gameManager ?? ServiceLocator.GetService<GameManager>()).StartGame();
        }
    }
}
