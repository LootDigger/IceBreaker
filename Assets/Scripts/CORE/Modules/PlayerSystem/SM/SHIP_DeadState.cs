using CORE.GameStates;
using CORE.Systems.PlayerSystem.Movement;
using Patterns.AbstractStateMachine;
using Patterns.ServiceLocator;

namespace CORE.Systems.PlayerSystem.SM
{
    public class SHIP_DeadState : IState
    {
        public StateMachine StateMachine { get; set; }
        
        private readonly PlayerRotation _playerRotation;
        private readonly PlayerMovement _playerMovement;
        private readonly CoreStateMachine _coreStateMachine;

        public SHIP_DeadState(StateMachine machine, PlayerRotation playerRotation,PlayerMovement playerMovement)
        {
            StateMachine = machine;
            _playerRotation = playerRotation;
            _playerMovement = playerMovement;
            _coreStateMachine = ServiceLocator.GetService<CoreStateMachine>();
        }

        public void EnterState()
        {
            _playerRotation.SetRotationBlock(true);
            _playerMovement.SetMovementBlock(true);
            _coreStateMachine.SetState<CORE_GameOverState>();
        }
    }
}
