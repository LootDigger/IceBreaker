using Core.ShipControls;
using CORE.Systems.PlayerSystem.Movement;
using Patterns.Command;
using Patterns.ServiceLocator;

namespace CORE.Systems.PlayerSystem.SM.Commands
{
    public class SetShipIdleStateCommand : ICommand
    {
        private readonly ShipSMPresenter _shipSMPresenter;
        private readonly PlayerRotation _playerRotation;
        private readonly PlayerMovement _playerMovement;

        public SetShipIdleStateCommand()
        {
            _shipSMPresenter = ServiceLocator.GetService<ShipSMPresenter>();
            _playerRotation = ServiceLocator.GetService<PlayerRotation>();
            _playerMovement = ServiceLocator.GetService<PlayerMovement>();
        }
        
        public void Execute()
        {
            _shipSMPresenter.SetState(new SHIP_IdleState());
            _playerRotation.SetRotationBlock(true);
            _playerMovement.SetMovementBlock(true);
        }
    }
}
