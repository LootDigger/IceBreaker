using System;
using CORE.Systems.PlayerSystem.Health;
using Patterns.AbstractStateMachine;
using Patterns.ServiceLocator;
using UnityEngine;

namespace CORE.Systems.PlayerSystem.SM
{
    public class SHIP_TakeDamageState : IState, IDisposable
    {
        public StateMachine StateMachine { get; set; }
        private HealthManager _healthManager;

        public SHIP_TakeDamageState(StateMachine stateMachine, HealthManager healthManager)
        {
            StateMachine = stateMachine;
            _healthManager = healthManager;
            SubscribeEvents();
        }
        
        public void EnterState()
        {
            SubscribeEvents();
            Debug.Log("SHIP_TakeDamageState EnterState");            
            _healthManager.DecreaseHealthPoint();
        }

        public void ExitState()
        {
            UnsubscribeEvents();
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
