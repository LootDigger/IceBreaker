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
      private GameWaiter _waiter;
      private Score _score;
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

      void Init()
      {
         ServiceLocator.RegisterService(this);
         _waiter = new GameWaiter();
         _score = new Score();
         _shipStateMachine = ServiceLocator.GetService<ShipStateMachine>();
         _coreStateMachine = ServiceLocator.GetService<CoreStateMachine>();
      }

      private void SubscribeEvents()
      {
         _shipStateMachine.SubscribeStateEnter<SHIP_DeadState>(OnPlayerDeath);
      }
      
      private void UnsubscribeEvents()
      {
         _shipStateMachine.UnsubscribeStateEnter<SHIP_DeadState>(OnPlayerDeath);
      }

      public void StartGame()
      {
         _coreStateMachine.SetState<CORE_GameplayState>();
         OnGameStartedHandler();
      }
      
      private void OnGameStartedHandler()
      {
         _waiter.StartWaiter();
      }

      public void OnPlayerDeath()
      {
         _score.ScoreValue = _waiter.StopWaiter();
      }
   }
}
