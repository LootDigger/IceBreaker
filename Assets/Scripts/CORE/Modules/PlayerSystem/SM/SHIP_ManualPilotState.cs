using CORE.Systems.PlayerSystem.Movement;
using Patterns.AbstractStateMachine;
using Patterns.Command;
using UnityEngine;

namespace CORE.Systems.PlayerSystem.SM
{
    public class SHIP_ManualPilotState : IState
    {
        private readonly PlayerRotation _playerRotation;
        private readonly PlayerMovement _playerMovement;
        
        public StateMachine StateMachine { get; set; }
        
        public SHIP_ManualPilotState(StateMachine stateMachine, PlayerRotation playerRotation,PlayerMovement playerMovement)
        {
            _playerRotation = playerRotation;
            _playerMovement = playerMovement;
            StateMachine = stateMachine;
        }

        public void EnterState()
        {
            Debug.Log("SHIP: Enter MANUAL State");
            _playerRotation.SetManualPilotDriver();
            _playerRotation.SetRotationBlock(false);
            _playerMovement.SetMovementBlock(false);
        }

        // MOVE TO EDGE DETECTOR UPDATE LOGIC
        public void UpdateState()
        {
          //  if (!_edgeDetector.IsLevelEdgeDetected()) { return; }
         //   CommandExecuter.ExecuteCommand(new SetAutoPilotStateCommand());
        }
    }
}
