using CORE.GameManager;
using CORE.GameStates;
using CORE.Modules.Player.SM;
using Patterns.AbstractStateMachine;
using Patterns.ServiceLocator;
using UI;
using UnityEngine;

namespace CORE.Gameplay
{
   public class GameManager : MonoBehaviour,IPresenter
   {
      [SerializeField]
      private GameClock _clock;

      private IView _uiManager;
      private Score _score;
      private Score _bestScore;
      private BestScoreSaver _bestScoreSaver;
      private StateMachine _shipStateMachine;
      private StateMachine _coreStateMachine;
      
      private void Start()
      {
         Init();
         SubscribeEvents();
      }

      private void OnDestroy()
      {
         UnsubscribeEvents();
      }

      private void Init()
      {
         ServiceLocator.RegisterService(this);
         _uiManager = ServiceLocator.GetService<UIManager>();
         InitUserScore();
         _shipStateMachine = ServiceLocator.GetService<ShipStateMachine>();
         _coreStateMachine = ServiceLocator.GetService<CoreStateMachine>();
      }

      private void InitUserScore()
      {
         _score = new Score();
         _bestScoreSaver = new BestScoreSaver(new JSONSaver());
         _bestScore = _bestScoreSaver.LoadScore();
         _bestScore ??= new Score();
      }

      private void SubscribeEvents()
      {
         _shipStateMachine.SubscribeStateEnter<SHIP_DeadState>(OnPlayerDeath);
         _clock.OnSecondPassed += ClockOnOnSecondPassed;
         _bestScore.OnScoreChangedEvent += OnBestScoreChangedEvent;
         _score.OnScoreChangedEvent += OnScoreChangedEvent;
      }

      private void UnsubscribeEvents()
      {
         _shipStateMachine.UnsubscribeStateEnter<SHIP_DeadState>(OnPlayerDeath);
         _clock.OnSecondPassed -= ClockOnOnSecondPassed;
         _bestScore.OnScoreChangedEvent -= OnBestScoreChangedEvent;
         _score.OnScoreChangedEvent -= OnScoreChangedEvent;
      }
      
      private void OnBestScoreChangedEvent(int score)
      {
         _uiManager.UpdateBestScoreView(score);
      }

      private void OnScoreChangedEvent(int score)
      {
         _uiManager.UpdateScoreView(score);
      }
      
      private void ClockOnOnSecondPassed(int currentScore)
      {
         _score.ScoreValue = currentScore;
      }
      
      private void OnGameStartedHandler()
      {
         _score.ScoreValue = 0;
         _clock.StartClock();
      }

      private void UpdateBestScore()
      {
         if(_score.ScoreValue <= _bestScore.ScoreValue) return;
         _bestScore.ScoreValue = _score.ScoreValue;
         _bestScoreSaver.SaveScore(_bestScore);
      }

      public void StartGame()
      {
         _coreStateMachine.SetState<CORE_GameplayState>();
         OnGameStartedHandler();
      }

      public void OnPlayerDeath()
      {
         _clock.StopClock();
         _score.ScoreValue = _clock.SecondsElapsed;
         UpdateBestScore();
      }
   }
}
