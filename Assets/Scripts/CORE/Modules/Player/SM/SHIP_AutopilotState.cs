using System;
using System.Threading.Tasks;
using CORE.Systems.PlayerSystem.Movement;
using Patterns.AbstractStateMachine;
using Patterns.ServiceLocator;
using UnityEngine;

namespace CORE.Modules.Player.SM
{
    public class SHIP_AutopilotState : IState
    {
        private readonly PlayerRotation _playerRotation;
        private readonly ShipStaticDataProvider _shipStaticDataProvider;

        public StateMachine StateMachine { get; set; }
        public Action OnEnterStateEvent { get; set; }
        public Action OnExitStateEvent { get; set; }

        public SHIP_AutopilotState(StateMachine currentSM, PlayerRotation playerRotation)
        {
            StateMachine = currentSM;
            _playerRotation = playerRotation;
            _shipStaticDataProvider = ServiceLocator.GetService<ShipStaticDataProvider>();
        }

        public void EnterState()
        {
            OnEnterStateEvent?.Invoke();
            _playerRotation.SetAutopilotDriver();
            StartAutopilotCountDown();
        }

        public void ExitState()
        {
            OnExitStateEvent?.Invoke();
        }

        private async Task StartAutopilotCountDown()
        {
            Debug.Log("autopilot dur: " + (int)(_shipStaticDataProvider.Data.AutopilotDuration * 1000));
            await Task.Delay((int)(_shipStaticDataProvider.Data.AutopilotDuration * 1000));
            SetControlsToManual();
        }

        private void SetControlsToManual()
        {
            StateMachine.SetState<SHIP_ManualPilotState>();
        }
    }
}
