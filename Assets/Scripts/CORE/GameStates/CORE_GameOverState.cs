using System;
using Core.PlayerCamera;
using Patterns.AbstractStateMachine;
using Patterns.ServiceLocator;

namespace CORE.GameStates
{
    public class CORE_GameOverState : IState
    {
        public StateMachine StateMachine { get; set; }
        public Action OnEnterStateEvent { get; set; }
        public Action OnExitStateEvent { get; set; }
        
        public CORE_GameOverState(StateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }
        
        public void EnterState()
        {
            OnEnterStateEvent?.Invoke();
            UIEventDocker.OnGameOverUIShown.Invoke();
        }

        public void ExitState()
        {
            OnExitStateEvent?.Invoke();
        }
        
    }
}
