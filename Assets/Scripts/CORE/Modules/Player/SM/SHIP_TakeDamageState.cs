using System;
using CORE.Systems.PlayerSystem.Health;
using Patterns.AbstractStateMachine;
using UnityEngine;

namespace CORE.Modules.Player.SM
{
    public class SHIP_TakeDamageState : IState, IDisposable
    {
        private HealthManager _healthManager;

        public StateMachine StateMachine { get; set; }
        public Action OnEnterStateEvent { get; set; }
        public Action OnExitStateEvent { get; set; }
        
        public SHIP_TakeDamageState(StateMachine stateMachine, HealthManager healthManager)
        {
            StateMachine = stateMachine;
            _healthManager = healthManager;
            SubscribeEvents();
        }
        
        public void EnterState()
        {
            OnEnterStateEvent?.Invoke();
            StateMachine.SetState(StateMachine.PreviousState);
            _healthManager.DecreaseHealthPoint();
        }

        public void ExitState()
        {
            OnExitStateEvent?.Invoke();
        }

        private void SubscribeEvents()
        {
            _healthManager.OnHealthReachedDeadPoint += OnHealthReachedDeadPointHandler;
        }
        
        private void UnsubscribeEvents()
        {
            _healthManager.OnHealthReachedDeadPoint -= OnHealthReachedDeadPointHandler;
        }

        private void OnHealthReachedDeadPointHandler()
        {
            StateMachine.SetState<SHIP_DeadState>();
        }

        public void Dispose() => UnsubscribeEvents();
    }
}
