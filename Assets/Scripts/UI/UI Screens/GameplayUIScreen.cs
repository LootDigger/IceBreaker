using CORE.Gameplay;
using UnityEngine.UIElements;

namespace UI
{
    public class GameplayUIScreen : UIScreen
    {
        private readonly TextElement _scoreText;
        private GameManager _gameManager;
        
        public GameplayUIScreen(VisualElement root) : base(root)
        {
            _scoreText = _rootElement.Q<TextElement>("SCORE-TEXT");
        }

        public void UpdateScoreView(int currentScore)
        {
            _scoreText.text = currentScore.ToString();
        }
    }
}