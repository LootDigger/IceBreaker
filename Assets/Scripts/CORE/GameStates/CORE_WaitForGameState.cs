using Patterns.AbstractStateMachine;
using Patterns.ServiceLocator;
using UnityEngine;

namespace CORE.GameStates
{
    public class CORE_WaitForGameState : IAbstractState
    {
        public void EnterState()
        {
            Debug.Log("CORE: Enter State WaitForGame");
        }
    }
}
