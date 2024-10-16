using Patterns.AbstractStateMachine;
using Patterns.ServiceLocator;
using UnityEngine;

namespace CORE.GameStates
{
    public class CORESM_InGame : AbstractState
    {
        private void Awake()
        {
            ServiceLocator.RegisterService(this);
        }

        public override void EnterState()
        {
            base.EnterState();
            Debug.Log("Enter State InGameState");
        }
    }
}
