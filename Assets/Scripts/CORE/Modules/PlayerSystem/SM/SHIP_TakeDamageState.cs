using CORE.Systems.PlayerSystem.Health;
using Patterns.AbstractStateMachine;
using Patterns.ServiceLocator;
using UnityEngine;

namespace CORE.Systems.PlayerSystem.SM
{
    public class SHIP_TakeDamageState : IState
    {
        public StateMachine StateMachine { get; set; }
        private HealthManager _healthManager;

        public SHIP_TakeDamageState(StateMachine stateMachine, HealthManager healthManager)
        {
            StateMachine = stateMachine;
            _healthManager = healthManager;
        }

        public virtual void EnterState()
        {
            Debug.Log("SHIP_TakeDamageState EnterState");            
            _healthManager.DecreaseHealthPoint();
        }
    }
}
