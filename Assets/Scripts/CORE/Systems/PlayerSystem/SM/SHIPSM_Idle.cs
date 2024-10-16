using Patterns.AbstractStateMachine;
using Patterns.Observer;
using Patterns.ServiceLocator;
using UnityEngine;

namespace CORE.Systems.PlayerSystem.SM
{
    public class SHIPSM_Idle : AbstractState, IStateObserver
    {
        private void Awake()
        {
            ServiceLocator.RegisterService(this);
        }

        public override void EnterState()
        {
            base.EnterState();
            Debug.Log("Enter state IDLE");
        }

        public void OnSubjectStateEnter(IStateSubject stateSubject)
        {
           // if(stateSubject is )
        }

        public void OnSubjectStateExit(IStateSubject stateSubject)
        {
        }
    }
}
