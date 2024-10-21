using Patterns.AbstractStateMachine;

namespace CORE.GameStates
{
    public class CoreStateMachine : StateMachine
    {
        public CoreStateMachine()
        {
            InitStateMachine();
        }
        
        private void InitStateMachine()
        {
            RegisterState(new CORE_BootstrapState(this));
            RegisterState(new CORE_InitState(this));
            RegisterState(new CORE_GameMenuState(this));
            RegisterState(new CORE_GameplayState(this));
            RegisterState(new CORE_GameOverState(this));
        }
    }
}
