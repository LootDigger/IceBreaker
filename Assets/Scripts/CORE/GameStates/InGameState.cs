using Patterns.AbstractStateMachine;
using Patterns.ServiceLocator;

namespace CORE.GameStates
{
    public class InGameState : AbstractState
    {
        private void Awake()
        {
            ServiceLocator.RegisterService(this);
        }
    }
}
