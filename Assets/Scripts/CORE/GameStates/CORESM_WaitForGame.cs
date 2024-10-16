using Patterns.AbstractStateMachine;
using Patterns.ServiceLocator;
using UnityEngine;

namespace CORE.GameStates
{
    public class CORESM_WaitForGame : AbstractState
    {
        private void Awake()
        {
            ServiceLocator.RegisterService(this);
        }
        
        public override void EnterState()
        {
            Debug.Log("Enter State WaitForGame");
            base.EnterState();
        }
    }
}
