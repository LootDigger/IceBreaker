using Core.ShipControls;
using CORE.Systems.PlayerSystem.Movement;
using Patterns.Command;
using Patterns.ServiceLocator;

namespace CORE.Systems.PlayerSystem.SM.Commands
{
    public class SetAutoPilotStateCommand : ICommand
    {
        private readonly ShipSMPresenter _shipSMPresenter;
        private readonly PlayerRotation _playerRotation;
        
        public SetAutoPilotStateCommand()
        {
            _shipSMPresenter = ServiceLocator.GetService<ShipSMPresenter>();
            _playerRotation = ServiceLocator.GetService<PlayerRotation>();
        }
        
        public void Execute()
        {
            _shipSMPresenter.SetState(new SHIP_AutopilotState());
            _playerRotation.SetAutopilotDriver();
        }
    }
}
