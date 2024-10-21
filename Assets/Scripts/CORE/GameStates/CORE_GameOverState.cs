using Patterns.AbstractStateMachine;
using Patterns.ServiceLocator;

namespace CORE.GameStates
{
    public class CORE_GameOverState : IState
    {
        public CORE_GameOverState(StateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        public StateMachine StateMachine { get; set; }
    }
}
