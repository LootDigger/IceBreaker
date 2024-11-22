using System;
using CORE.GameStates;
using CORE.Modules.Player.Movement;
using CORE.Systems.PlayerSystem.Movement;
using Patterns.AbstractStateMachine;
using Patterns.Command;
using Patterns.ServiceLocator;
using System.Threading.Tasks;
using Core.PlayerCamera;

namespace CORE.Modules.Player.SM
{
    public class SHIP_DeadState : IState
    {
        private readonly CommandExecuter _commandExecuter;
        private IGameCamera GameCamera => ServiceLocator.GetService<IGameCamera>();
        
        public StateMachine StateMachine { get; set; }
        public Action OnEnterStateEvent { get; set; }
        public Action OnExitStateEvent { get; set; }

        private readonly PlayerRotation _playerRotation;
        private readonly PlayerMovement _playerMovement;
        private readonly CoreStateMachine _coreStateMachine;
        private readonly AnimationInvoker _sinkAnimation;

        public SHIP_DeadState(StateMachine machine, PlayerRotation playerRotation,PlayerMovement playerMovement, AnimationInvoker sinkAnimation)
        {
            StateMachine = machine;
            _playerRotation = playerRotation;
            _playerMovement = playerMovement;
            _sinkAnimation = sinkAnimation;
            _coreStateMachine = ServiceLocator.GetService<CoreStateMachine>();
            _commandExecuter = ServiceLocator.GetService<CommandExecuter>();
        }

        public void EnterState()
        {
            OnEnterStateEvent?.Invoke();
            GameCamera.SetTargetFollowState(false);
            _sinkAnimation.Play();
            _playerRotation.SetRotationBlock(true);
            _playerMovement.SetMovementBlock(true);
            GameOverDelay();
        }

        public void ExitState()
        {
            OnExitStateEvent?.Invoke();
        }

        private async Task GameOverDelay()
        {
            await Task.Delay(1000);
            _coreStateMachine.SetState<CORE_GameOverState>();

        }
    }
}
