using System;
using CORE.Gameplay;
using CORE.Systems.PlayerSystem.Health;
using Patterns.AbstractStateMachine;
using Patterns.Command;
using Patterns.ServiceLocator;

namespace CORE.Modules.Player.SM
{
    public class SHIP_GameInitState : IState
    {       
        private HealthManager _healthManager;
        private PlayerTransform _playerTransform;

        public StateMachine StateMachine { get; set; }
        public Action OnEnterStateEvent { get; set; }
        public Action OnExitStateEvent { get; set; }

        public SHIP_GameInitState(StateMachine stateMachine, HealthManager healthManager, PlayerTransform playerTransform)
        {
            StateMachine = stateMachine;
            _healthManager = healthManager;
            _playerTransform = playerTransform;
        }

        public void EnterState()
        {
            OnEnterStateEvent?.Invoke();
            _healthManager.ResetHealthPoints();
            _playerTransform.ResetTransform();
            StateMachine.SetState<SHIP_ManualPilotState>();
        }

        public void ExitState()
        {
            OnExitStateEvent?.Invoke();
        }
    }
}
