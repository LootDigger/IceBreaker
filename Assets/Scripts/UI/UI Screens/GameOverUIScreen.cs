using CORE.Gameplay;
using Patterns.ServiceLocator;
using UnityEngine.UIElements;

namespace UI
{
    public class GameOverUIScreen : UIScreen
    {
        private readonly TextElement _scoreText;
        private readonly TextElement _bestScoreText;

        private Button _replayButton;
        private GameManager _gameManager;

        public GameOverUIScreen(VisualElement root) : base(root)
        {
            _scoreText = _rootElement.Q<TextElement>("CURRENT-SCORE");
            _bestScoreText = _rootElement.Q<TextElement>("BEST-SCORE");
            
            _replayButton = _rootElement.Q<Button>("PLAY-AGAIN-BUTTON");
            _eventRegister.RegisterCallback<ClickEvent>(_replayButton, () =>
            {
                UIEventDocker.OnGameplayUIShown.Invoke();
                RestartGame();
            });
        }
        
        private void RestartGame()
        {
            if (_gameManager == null)
            {
                _gameManager = ServiceLocator.GetService<GameManager>();
            }
            _gameManager .StartGame();
        }

        public void UpdateScoreView(int score)
        {
            _scoreText.text = score.ToString();
        }
        
        public void UpdateBestView( int bestScore)
        {
            _bestScoreText.text = bestScore.ToString();
        }
    }
}
