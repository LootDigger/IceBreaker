using Patterns.AbstractStateMachine;
using Patterns.ServiceLocator;
using Scene_Management;

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
            RegisterState(new CORE_InitState(this,ServiceLocator.GetService<SceneLoader>()));
            RegisterState(new CORE_GameMenuState(this));
            RegisterState(new CORE_GameplayState(this));
            RegisterState(new CORE_GameOverState(this));
        }
    }
}
