using Patterns.AbstractStateMachine;
using System.Threading.Tasks;
using CORE.Systems.PlayerSystem.Movement;
using Patterns.Command;
using Patterns.ServiceLocator;
using UnityEngine;

namespace CORE.Systems.PlayerSystem.SM
{
    public class SHIP_AutopilotState : IState
    {
        private readonly PlayerRotation _playerRotation;
        public StateMachine StateMachine { get; set; }

        public SHIP_AutopilotState(StateMachine currentSM, PlayerRotation playerRotation)
        {
            StateMachine = currentSM;
            _playerRotation = playerRotation;
        }

        public void EnterState()
        {
            Debug.Log("SHIP Enter Autopilot State");
            _playerRotation.SetAutopilotDriver();
            StartAutopilotCountDown();
        }

        private async Task StartAutopilotCountDown()
        {
            await Task.Delay(5000);
            SetControlsToManual();
        }

        private void SetControlsToManual()
        {
            StateMachine.SetState<SHIP_ManualPilotState>();
        }
    }
}
