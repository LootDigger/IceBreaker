using System;
using System.Threading.Tasks;
using CORE.Modules.Player.Movement;
using CORE.Systems.PlayerSystem.Movement;
using Patterns.AbstractStateMachine;
using Patterns.ServiceLocator;
using UnityEngine;

namespace CORE.Modules.Player.SM
{
    public class SHIP_AutopilotState : IState
    {
        private readonly PlayerMovement _playerMovement;
        private readonly PlayerRotation _playerRotation;
        private readonly ShipStaticDataProvider _shipStaticDataProvider;

        public StateMachine StateMachine { get; set; }
        public Action OnEnterStateEvent { get; set; }
        public Action OnExitStateEvent { get; set; }

        public SHIP_AutopilotState(StateMachine currentSM, PlayerMovement playerMovement, PlayerRotation playerRotation)
        {
            StateMachine = currentSM;
            _playerMovement = playerMovement;
            _playerRotation = playerRotation;
            _shipStaticDataProvider = ServiceLocator.GetService<ShipStaticDataProvider>();
        }

        public void EnterState()
        {
            OnEnterStateEvent?.Invoke();
            _playerMovement.SetMovementBlock(false);
            _playerRotation.SetRotationBlock(false);
            _playerRotation.SetAutopilotDriver();
            StartAutopilotCountDown();
        }

        public void ExitState()
        {
            OnExitStateEvent?.Invoke();
        }

        private async Task StartAutopilotCountDown()
        {
            await Task.Delay((int)(_shipStaticDataProvider.Data.AutopilotDuration * 1000));
            SetControlsToManual();
        }

        private void SetControlsToManual()
        {
            StateMachine.SetState<SHIP_ManualPilotState>();
        }
    }
}
