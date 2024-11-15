using System;
using CORE.Modules.Player.SM;
using Patterns.AbstractStateMachine;
using Patterns.ServiceLocator;

namespace CORE.GameStates
{
    public class CORE_GameplayState : IState
    {
        public StateMachine StateMachine { get; set; }
        public Action OnEnterStateEvent { get; set; }
        public Action OnExitStateEvent { get; set; }
        private ShipStateMachine _shipStateMachine;
        
        public CORE_GameplayState(StateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        public void EnterState()
        {
            OnEnterStateEvent?.Invoke();
            _shipStateMachine = ServiceLocator.GetService<ShipStateMachine>();
            _shipStateMachine.SetState<SHIP_GameInitState>();
        }

        public void ExitState()
        {
            OnExitStateEvent?.Invoke();
        }
    }
}
