using Patterns.AbstractStateMachine;
using Patterns.ServiceLocator;
using UnityEngine;

namespace CORE.Systems.PlayerSystem.SM
{
    public class SHIP_TakeDamageState : IAbstractState
    {
        public virtual void EnterState()
        {
            Debug.Log("SHIP_TakeDamageState EnterState");            
        }
    }
}
