using CORE.Systems.PlayerSystem.SM.Commands;
using Patterns.Command;

namespace CORE.GameStates.Commands
{
    public class SetInGameStateCommand : ICommand
    {
        public void Execute()
        {
            CommandExecuter.ExecuteCommand(new SetManualPilotStateCommand());
        }
    }
}
