using System;
using Patterns.AbstractStateMachine;

namespace CORE.Modules.Player.SM
{
    public class SHIP_ShootState : IState
    {
        public StateMachine StateMachine { get; set; }
        public Action OnEnterStateEvent { get; set; }
        public Action OnExitStateEvent { get; set; }
        
        public void EnterState()
        {
            OnEnterStateEvent?.Invoke();
        }

        public void ExitState()
        {
            OnExitStateEvent?.Invoke();
        }

        public SHIP_ShootState(StateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }
    }
}
