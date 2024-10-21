using Patterns.AbstractStateMachine;
using Patterns.ServiceLocator;
using UnityEngine;

namespace CORE.GameStates
{
    public class CORE_GameMenuState : IState
    {
        public StateMachine StateMachine { get; set; }

        public CORE_GameMenuState(StateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }
        
        public void EnterState()
        {
            Debug.Log("CORE: Enter State WaitForGame"  + Time.time);
        }
    }
}
