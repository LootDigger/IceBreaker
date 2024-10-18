namespace Patterns.Command
{
    public static class CommandExecuter
    {
        public static void ExecuteCommand(ICommand command)
        {
            command.Execute();
        }
    }
}
