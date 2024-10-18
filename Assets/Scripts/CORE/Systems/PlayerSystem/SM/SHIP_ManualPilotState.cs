using CORE.Systems.PlayerSystem.SM.Commands;
using Patterns.AbstractStateMachine;
using Patterns.Command;
using Patterns.ServiceLocator;
using UnityEngine;

namespace CORE.Systems.PlayerSystem.SM
{
    public class SHIP_ManualPilotState : IAbstractState
    {
        private MapEdgeDetector _edgeDetector;

        public SHIP_ManualPilotState()
        {
            _edgeDetector = ServiceLocator.GetService<MapEdgeDetector>();
        }

        public void EnterState()
        {
            Debug.Log("SHIP: Enter MANUAL State");
        }

        public void UpdateState()
        {
            if (!_edgeDetector.IsLevelEdgeDetected()) { return; }
            CommandExecuter.ExecuteCommand(new SetAutoPilotStateCommand());
        }
    }
}
