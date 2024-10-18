using CORE.Systems.PlayerSystem.SM.Commands;
using Patterns.Command;
using Patterns.ServiceLocator;

namespace CORE.GameStates.Commands
{
    public class SetInitStateCommand : ICommand
    {
        private CoreSMPresenter _corePresenter;

        public SetInitStateCommand()
        {
            _corePresenter = ServiceLocator.GetService<CoreSMPresenter>();
        }
        
        public void Execute()
        {
            _corePresenter.SetState(new CORE_InitState());
            CommandExecuter.ExecuteCommand(new SetShipIdleStateCommand());
        }
    }
}
