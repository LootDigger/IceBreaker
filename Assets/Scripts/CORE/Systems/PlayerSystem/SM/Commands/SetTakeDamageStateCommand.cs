using CORE.Systems.PlayerSystem.Health;
using Patterns.Command;
using Patterns.ServiceLocator;

namespace CORE.Systems.PlayerSystem.SM.Commands
{
    public class SetTakeDamageStateCommand : ICommand
    {
        private HealthManager _healthManager;

        public SetTakeDamageStateCommand()
        {
            _healthManager = ServiceLocator.GetService<HealthManager>();
        }
        
        public void Execute()
        {
            _healthManager.DecreaseHealthPoint();
        }
    }
}
