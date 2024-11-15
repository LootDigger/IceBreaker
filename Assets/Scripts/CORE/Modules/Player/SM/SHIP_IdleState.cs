using System;
using CORE.Modules.Player.Movement;
using CORE.Systems.PlayerSystem.Movement;
using Patterns.AbstractStateMachine;

namespace CORE.Modules.Player.SM
{
    public class SHIP_IdleState : IState
    {
        private readonly PlayerRotation _playerRotation;
        private readonly PlayerMovement _playerMovement;
        
        public StateMachine StateMachine { get; set; }
        
        public Action OnEnterStateEvent { get; set; }
        public Action OnExitStateEvent { get; set; }

        public SHIP_IdleState(StateMachine stateMachine,PlayerRotation playerRotation, PlayerMovement playerMovement)
        {
            StateMachine = stateMachine;
            _playerRotation = playerRotation;
            _playerMovement = playerMovement;
        }

        public void EnterState()
        {
            OnEnterStateEvent?.Invoke();
            _playerRotation.SetRotationBlock(true);
            _playerMovement.SetMovementBlock(true);
        }

        public void ExitState()
        {
            OnExitStateEvent?.Invoke();
        }
    }
}
