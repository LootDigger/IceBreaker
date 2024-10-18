using Patterns.AbstractStateMachine;
using Patterns.ServiceLocator;

namespace CORE.GameStates
{
    public class CORE_GameOverState : IAbstractState
    {
        private void Awake()
        {
            ServiceLocator.RegisterService(this);
        }
    }
}
