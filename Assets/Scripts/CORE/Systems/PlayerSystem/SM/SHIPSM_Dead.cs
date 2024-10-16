using Patterns.AbstractStateMachine;
using Patterns.ServiceLocator;

namespace CORE.Systems.PlayerSystem.SM
{
    public class SHIPSM_Dead : AbstractState
    {
        private void Awake()
        {
            ServiceLocator.RegisterService(this);
        }
    }
}
