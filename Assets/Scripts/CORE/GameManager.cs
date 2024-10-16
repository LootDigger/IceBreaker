using System;
using CORE.GameStates;
using Patterns.AbstractStateMachine;
using Patterns.ServiceLocator;
using UnityEngine;

namespace CORE
{
   public class GameManager : MonoBehaviour
   {
      private StateMachine _stateMachine;
      
      private void Awake() => _stateMachine = new();
      private void Start() => _stateMachine.SetState(ServiceLocator.GetService<CORESM_Init>());
      private void Update() => _stateMachine.UpdateMachine();
      
      public void SetGameState(AbstractState newState) => _stateMachine.SetState(newState);
   }
}
