using System;
using CORE.Gameplay;
using CORE.GameStates;
using CORE.Modules.Player.Movement;
using CORE.Systems.PlayerSystem.Movement;
using Patterns.AbstractStateMachine;
using Patterns.Command;
using Patterns.ServiceLocator;

namespace CORE.Modules.Player.SM
{
    public class SHIP_DeadState : IState
    {
        private readonly CommandExecuter _commandExecuter;

        public StateMachine StateMachine { get; set; }
        public Action OnEnterStateEvent { get; set; }
        public Action OnExitStateEvent { get; set; }

        private readonly PlayerRotation _playerRotation;
        private readonly PlayerMovement _playerMovement;
        private readonly CoreStateMachine _coreStateMachine;

        public SHIP_DeadState(StateMachine machine, PlayerRotation playerRotation,PlayerMovement playerMovement)
        {
            StateMachine = machine;
            _playerRotation = playerRotation;
            _playerMovement = playerMovement;
            _coreStateMachine = ServiceLocator.GetService<CoreStateMachine>();
            _commandExecuter = ServiceLocator.GetService<CommandExecuter>();
        }

        public void EnterState()
        {
            OnEnterStateEvent?.Invoke();
            _playerRotation.SetRotationBlock(true);
            _playerMovement.SetMovementBlock(true);
            _coreStateMachine.SetState<CORE_GameOverState>();
        }

        public void ExitState()
        {
            OnExitStateEvent?.Invoke();
        }
    }
}
