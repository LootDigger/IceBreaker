using System;
using CORE.Gameplay;
using Patterns.AbstractStateMachine;
using Patterns.Command;
using Patterns.ServiceLocator;

namespace CORE.Modules.Player.SM
{
    public class SHIP_GameInitState : IState
    {
        public StateMachine StateMachine { get; set; }
        public Action OnEnterStateEvent { get; set; }
        public Action OnExitStateEvent { get; set; }

        public SHIP_GameInitState(StateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        public void EnterState()
        {
            OnEnterStateEvent?.Invoke();
            StateMachine.SetState<SHIP_ManualPilotState>();
        }

        public void ExitState()
        {
            OnExitStateEvent?.Invoke();
        }
    }
}
