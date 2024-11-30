using CORE.GameManager;
using CORE.GameStates;
using CORE.Modules.Player.SM;
using Patterns.AbstractStateMachine;
using Patterns.ServiceLocator;
using UI;
using UnityEngine;

namespace CORE.Gameplay
{
   public class GameManager : MonoBehaviour
   {
      [SerializeField]
      private GameClock _clock;

      private IView _uiManager;
      private IModel<int> _scoreModel;
      private IModel<int> _bestScoreModel;
      
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
         _scoreModel = new Score();
         _bestScoreSaver = new BestScoreSaver(new JSONSaver());
         _bestScoreModel = _bestScoreSaver.LoadScore();
         _bestScoreModel ??= new Score();
         _uiManager.UpdateBestScoreView(_bestScoreModel.Value);
      }

      private void SubscribeEvents()
      {
         _shipStateMachine.SubscribeStateEnter<SHIP_DeadState>(OnPlayerDeath);
         _clock.OnSecondPassed += ClockOnOnSecondPassed;
         _bestScoreModel.OnModelChangedEvent += OnBestScoreChangedEvent;
         _scoreModel.OnModelChangedEvent += OnScoreChangedEvent;
      }

      private void UnsubscribeEvents()
      {
         _shipStateMachine.UnsubscribeStateEnter<SHIP_DeadState>(OnPlayerDeath);
         _clock.OnSecondPassed -= ClockOnOnSecondPassed;
         _bestScoreModel.OnModelChangedEvent -= OnBestScoreChangedEvent;
         _scoreModel.OnModelChangedEvent -= OnScoreChangedEvent;
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
         _scoreModel.Value = currentScore;
      }
      
      private void OnGameStartedHandler()
      {
         _scoreModel.Value = 0;
         _clock.StartClock();
      }

      private void UpdateBestScore()
      {
         if(_scoreModel.Value <= _bestScoreModel.Value) return;
         _bestScoreModel.Value = _scoreModel.Value;
         _bestScoreSaver.SaveScore(_bestScoreModel as Score);
      }

      public void StartGame()
      {
         _coreStateMachine.SetState<CORE_GameplayState>();
         OnGameStartedHandler();
      }

      public void OnPlayerDeath()
      {
         _clock.StopClock();
         _scoreModel.Value = _clock.SecondsElapsed;
         UpdateBestScore();
      }
   }
}
