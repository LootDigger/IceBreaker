using System;
using CORE.GameManager;
using CORE.GameStates;
using CORE.Modules.Player.SM;
using Patterns.AbstractStateMachine;
using Patterns.ServiceLocator;
using UnityEngine;

namespace CORE.Gameplay
{
   public class GameManager : MonoBehaviour
   {
      [SerializeField]
      private GameClock _clock;
      
      private Score _score;
      private Score _bestScore;
      private BestScoreSaver _bestScoreSaver;

      private StateMachine _shipStateMachine;
      private StateMachine _coreStateMachine;
      
      public event Action<int> OnGameScoreChanged;
      
      private void Start()
      {
         Init();
         SubscribeEvents();
      }

      private void OnDestroy()
      {
         UnsubscribeEvents();
      }

      void Init()
      {
         ServiceLocator.RegisterService(this);
         InitUserScore();
         _shipStateMachine = ServiceLocator.GetService<ShipStateMachine>();
         _coreStateMachine = ServiceLocator.GetService<CoreStateMachine>();
      }

      void InitUserScore()
      {
         _score = new Score();
         _bestScoreSaver = new BestScoreSaver(new JSONSaver());
         _bestScore = _bestScoreSaver.LoadScore();
         if (_bestScore != null) return;
         _bestScore = new Score();
      }

      private void SubscribeEvents()
      {
         _shipStateMachine.SubscribeStateEnter<SHIP_DeadState>(OnPlayerDeath);
         _clock.OnSecondPassed += ClockOnOnSecondPassed;
      }

      private void UnsubscribeEvents()
      {
         _shipStateMachine.UnsubscribeStateEnter<SHIP_DeadState>(OnPlayerDeath);
         _clock.OnSecondPassed -= ClockOnOnSecondPassed;
      }
      
      private void ClockOnOnSecondPassed(int currentScore)
      {
         _score.ScoreValue = currentScore;
         OnGameScoreChanged.Invoke(currentScore);
      }

      public void StartGame()
      {
         _coreStateMachine.SetState<CORE_GameplayState>();
         OnGameStartedHandler();
      }
      
      private void OnGameStartedHandler()
      {
         _clock.StartClock();
         OnGameScoreChanged.Invoke(0);
      }

      private void UpdateBestScore()
      {
         if(_score.ScoreValue <= _bestScore.ScoreValue) return;
         _bestScore.ScoreValue = _score.ScoreValue;
         _bestScoreSaver.SaveScore(_bestScore);
      }

      public void OnPlayerDeath()
      {
         _clock.StopClock();
         _score.ScoreValue = _clock.SecondsElapsed;
         UpdateBestScore();
      }
   }
}
