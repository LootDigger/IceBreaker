using System;
using System.Collections.Generic;
using Patterns.AbstractStateMachine;

namespace Patterns.Command
{
    public class CommandExecuter
    {
        private Dictionary<Type, ICommand> _registeredCommands;
        
        public CommandExecuter()
        {
            _registeredCommands = new();
        }

        public void RegisterCommand(ICommand command)
        {
            _registeredCommands.Add(command.GetType(), command);
        }
        
        public void ExecuteCommand<TState>() where TState : ICommand
        {
            _registeredCommands[typeof(TState)].Execute();
        }
    }
}
