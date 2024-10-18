using Patterns.Command;
using Patterns.ServiceLocator;

namespace CORE.GameStates.Commands
{
    public class SetWaitForGameStateCommand : ICommand
    {        
        private CoreSMPresenter _corePresenter;
        
        public SetWaitForGameStateCommand()
        {
            _corePresenter = ServiceLocator.GetService<CoreSMPresenter>();
        }
        
        public void Execute()
        {
            _corePresenter.SetState(new CORE_WaitForGameState());
        }
    }
}
