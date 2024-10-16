using Patterns.AbstractStateMachine;
using Patterns.ServiceLocator;

namespace CORE.GameStates
{
    public class WaitForStartState : AbstractState
    {
        
        private void Awake()
        {
            ServiceLocator.RegisterService(this);
        }
        
        public override void EnterState()
        {
            base.EnterState();
        }
    }
}
