using CORE.GameStates.Commands;
using Patterns.Command;
using Patterns.ServiceLocator;

namespace CORE
{
    public class CoreSMPresenter : AbstractSMPresenter
    { 
        void Start()
        {
            base.Start();
            ServiceLocator.RegisterService(this);
            CommandExecuter.ExecuteCommand(new SetInitStateCommand());
        }
    }
}
