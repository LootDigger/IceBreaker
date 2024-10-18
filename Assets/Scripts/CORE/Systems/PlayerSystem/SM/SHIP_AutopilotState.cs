using Patterns.AbstractStateMachine;
using System.Threading.Tasks;
using CORE.Systems.PlayerSystem.SM.Commands;
using Patterns.Command;
using UnityEngine;

namespace CORE.Systems.PlayerSystem.SM
{
    public class SHIP_AutopilotState : IAbstractState
    {
        public void EnterState()
        {
            Debug.Log("SHIP Enter Autopilot State");
            StartAutopilotCountDown();
        }

        private async Task StartAutopilotCountDown()
        {
            await Task.Delay(5000);
            SetControlsToManual();
        }

        private void SetControlsToManual()
        {
            CommandExecuter.ExecuteCommand(new SetManualPilotStateCommand());
        }
    }
}
