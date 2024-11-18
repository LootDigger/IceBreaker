using System;
using Patterns.AbstractStateMachine;

namespace CORE.GameStates
{
    public class CORE_GameMenuState : IState
    {
        public StateMachine StateMachine { get; set; }
        public Action OnEnterStateEvent { get; set; }
        public Action OnExitStateEvent { get; set; }

        public CORE_GameMenuState(StateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }
        
        public void EnterState()
        {
            OnEnterStateEvent?.Invoke();
            UIEventDocker.OnMainMenuUIShown.Invoke();
        }

        public void ExitState()
        {
            OnExitStateEvent?.Invoke();
        }
    }
}
