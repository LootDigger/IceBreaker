using Patterns.AbstractStateMachine;
using Patterns.ServiceLocator;

namespace CORE.GameStates
{
    public class GameOverState : AbstractState
    {
        private void Awake()
        {
            ServiceLocator.RegisterService(this);
        }
    }
}
