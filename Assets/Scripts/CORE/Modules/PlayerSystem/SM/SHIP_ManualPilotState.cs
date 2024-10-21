using System;
using CORE.Systems.PlayerSystem.Movement;
using Patterns.AbstractStateMachine;
using UnityEngine;

namespace CORE.Systems.PlayerSystem.SM
{
    public class SHIP_ManualPilotState : IState, IDisposable
    {
        private readonly PlayerRotation _playerRotation;
        private readonly PlayerMovement _playerMovement;
        private readonly MapEdgeDetector _mapEdgeDetector;
        
        public StateMachine StateMachine { get; set; }
        
        public SHIP_ManualPilotState(StateMachine stateMachine, PlayerRotation playerRotation,PlayerMovement playerMovement, MapEdgeDetector mapEdgeDetector)
        {
            _playerRotation = playerRotation;
            _playerMovement = playerMovement;
            StateMachine = stateMachine;
            _mapEdgeDetector = mapEdgeDetector;
        }

        public void EnterState()
        {
            SubscribeEvents();
            Debug.Log("SHIP: Enter MANUAL State");
            _playerRotation.SetManualPilotDriver();
            _playerRotation.SetRotationBlock(false);
            _playerMovement.SetMovementBlock(false);
        }

        public void ExitState()
        {
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
            StateMachine.SetState<SHIP_AutopilotState>();
        }
    }
}
