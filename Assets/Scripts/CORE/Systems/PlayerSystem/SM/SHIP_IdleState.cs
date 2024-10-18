using Patterns.AbstractStateMachine;
using UnityEngine;

namespace CORE.Systems.PlayerSystem.SM
{
    public class SHIP_IdleState : IAbstractState
    {
        public void EnterState()
        {
            Debug.Log("SHIP: Enter state IDLE");
        }
    }
}
