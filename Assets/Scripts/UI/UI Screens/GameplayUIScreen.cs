using System;
using CORE.Gameplay;
using Patterns.ServiceLocator;
using UnityEngine;
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
        
        public override void ShowInstantly()
        { 
            SubscribeGameManagerEvent();
            base.ShowInstantly();
        }

        public override void HideInstantly()
        {
            UnsubscribeGameManagerEvent();
            base.HideInstantly();
        }

        void SubscribeGameManagerEvent()
        {
            (_gameManager ?? ServiceLocator.GetService<GameManager>()).OnGameScoreChanged += GameManagerOnOnGameScoreChanged;
        }

        void UnsubscribeGameManagerEvent()
        {
            if(_gameManager == null) return;
            _gameManager.OnGameScoreChanged += GameManagerOnOnGameScoreChanged;
        }

        private void GameManagerOnOnGameScoreChanged(int currentScore)
        {
            _scoreText.text = currentScore.ToString();
        }
    }
}