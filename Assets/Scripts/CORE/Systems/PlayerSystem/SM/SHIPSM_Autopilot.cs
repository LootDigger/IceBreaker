using System;
using Patterns.AbstractStateMachine;
using Patterns.ServiceLocator;
using System.Threading.Tasks;
using Core.ShipControls;
using UnityEngine;

namespace CORE.Systems.PlayerSystem.SM
{
    public class SHIPSM_Autopilot : AbstractState
    {
        private void Awake()
        {
            ServiceLocator.RegisterService(this);
        }

        public override void EnterState()
        {
            Debug.Log("Enter Autopilot State");
            base.EnterState();
            StartAutopilotCountDown();
        }


        private async Task StartAutopilotCountDown()
        {
            Debug.LogWarning("Make it explicit");
            await Task.Delay(5000);
            SetControlsToManual();
        }

        private void SetControlsToManual()
        {
            ServiceLocator.GetService<ShipSMPresenter>().SetState(ServiceLocator.GetService<SHIPSM_ManualControl>());
        }
    }
}
