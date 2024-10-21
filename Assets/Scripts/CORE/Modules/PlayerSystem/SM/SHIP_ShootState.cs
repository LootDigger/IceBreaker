using Patterns.AbstractStateMachine;
using Patterns.ServiceLocator;

namespace CORE.Systems.PlayerSystem.SM
{
    public class SHIP_ShootState : IState
    {
        public StateMachine StateMachine { get; set; }

        public SHIP_ShootState(StateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }
    }
}
