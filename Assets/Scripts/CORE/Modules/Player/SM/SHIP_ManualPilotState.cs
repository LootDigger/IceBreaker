using System;
using CORE.Modules.Player.Movement;
using CORE.Systems.PlayerSystem;
using CORE.Systems.PlayerSystem.Movement;
using Patterns.AbstractStateMachine;
using UnityEngine;

namespace CORE.Modules.Player.SM
{
    public class SHIP_ManualPilotState : IState, IDisposable
    {
        private readonly PlayerRotation _playerRotation;
        private readonly PlayerMovement _playerMovement;
        private readonly MapEdgeDetector _mapEdgeDetector;
        
        public StateMachine StateMachine { get; set; }
        public Action OnEnterStateEvent { get; set; }
        public Action OnExitStateEvent { get; set; }

        public SHIP_ManualPilotState(StateMachine stateMachine, PlayerRotation playerRotation,PlayerMovement playerMovement, MapEdgeDetector mapEdgeDetector)
        {
            _playerRotation = playerRotation;
            _playerMovement = playerMovement;
            StateMachine = stateMachine;
            _mapEdgeDetector = mapEdgeDetector;
        }

        public void EnterState()
        {
            Debug.Log("$ Enter State");
            OnEnterStateEvent?.Invoke();
            SubscribeEvents();
            _playerRotation.SetManualPilotDriver();
            _playerRotation.SetRotationBlock(false);
            _playerMovement.SetMovementBlock(false);
        }

        public void ExitState()
        {
            Debug.Log("$ Exit State");
            OnExitStateEvent?.Invoke();
            UnsubscribeEvents();
        }

        public void Dispose()
        {
            UnsubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _mapEdgeDetector.OnMapEdgeReached += OnMapEdgeReachedHandler;
        }
        
        private void UnsubscribeEvents()
        {
            _mapEdgeDetector.OnMapEdgeReached -= OnMapEdgeReachedHandler;
        }

        private void OnMapEdgeReachedHandler()
        {
            Debug.Log("$ SHIP_ManualPilotState: OnMapEdgeReachedHandler");
            StateMachine.SetState<SHIP_AutopilotState>();
        }
    }
}
