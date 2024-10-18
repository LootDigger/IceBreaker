using Patterns.Command;
using UnityEngine;

namespace CORE.GameStates.Commands
{
    public class SetGameOverStateCommand : ICommand
    {
        public void Execute()
        {
           Debug.Log("SetGameOverStateCommand");
        }
    }
}
