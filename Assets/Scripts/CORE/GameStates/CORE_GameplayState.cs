using CORE.Modules.PlayerSystem.SM;
using Patterns.AbstractStateMachine;
using Patterns.ServiceLocator;
using UnityEngine;

namespace CORE.GameStates
{
    public class CORE_GameplayState : IState
    {
        public StateMachine StateMachine { get; set; }
        private ShipStateMachine _shipStateMachine;
        
        public CORE_GameplayState(StateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        public void EnterState()
        {
            Debug.Log("CORE: Enter State InGameState");
            _shipStateMachine = ServiceLocator.GetService<ShipStateMachine>();
            _shipStateMachine.SetState<SHIP_GameInitState>();
        }
    }
}
