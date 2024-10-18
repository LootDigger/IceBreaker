using Patterns.AbstractStateMachine;
using Patterns.ServiceLocator;
using UnityEngine;

namespace CORE.GameStates
{
    public class CORE_InGameState : IAbstractState
    {
        public void EnterState()
        {
            Debug.Log("CORE: Enter State InGameState");
        }
    }
}
