using Core.ShipControls;
using Patterns.AbstractStateMachine;
using Patterns.ServiceLocator;
using UnityEngine;

namespace CORE.Systems.PlayerSystem.SM
{
    public class SHIPSM_ManualControl : AbstractState
    {
        [SerializeField]
        private MapEdgeDetector _edgeDetector;
        
        private void Awake()
        {
            ServiceLocator.RegisterService(this);
        }

        public override void EnterState()
        {
            Debug.Log("Enter MANUAL State");
        }

        public override void UpdateState()
        {
            if (_edgeDetector.IsLevelEdgeDetected())
            {
                ServiceLocator.GetService<ShipSMPresenter>().SetState(ServiceLocator.GetService<SHIPSM_Autopilot>());
            }
        }
    }
}
