using Core.ShipControls;
using CORE.Systems.PlayerSystem.Movement;
using Patterns.Command;
using Patterns.ServiceLocator;

namespace CORE.Systems.PlayerSystem.SM.Commands
{
    public class SetManualPilotStateCommand : ICommand
    {
        private readonly ShipSMPresenter _shipSMPresenter;
        private readonly PlayerRotation _playerRotation;
        private readonly PlayerMovement _playerMovement;
        
        public SetManualPilotStateCommand()
        {
            _shipSMPresenter = ServiceLocator.GetService<ShipSMPresenter>();
            _playerRotation = ServiceLocator.GetService<PlayerRotation>();
            _playerMovement = ServiceLocator.GetService<PlayerMovement>();
        }
        
        public void Execute()
        {
            _shipSMPresenter.SetState(new SHIP_ManualPilotState());
            _playerRotation.SetManualPilotDriver();
            _playerRotation.SetRotationBlock(false);
            _playerMovement.SetMovementBlock(false);
        }
    }
}
