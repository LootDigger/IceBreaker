using CORE.Systems.PlayerSystem.Movement;
using Patterns.AbstractStateMachine;
using UnityEngine;

namespace CORE.Systems.PlayerSystem.SM
{
    public class SHIP_IdleState : IState
    {
        private readonly PlayerRotation _playerRotation;
        private readonly PlayerMovement _playerMovement;
        
        public StateMachine StateMachine { get; set; }
        
        public SHIP_IdleState(StateMachine stateMachine,PlayerRotation playerRotation, PlayerMovement playerMovement)
        {
            StateMachine = stateMachine;
            _playerRotation = playerRotation;
            _playerMovement = playerMovement;
        }

        public void EnterState()
        {
            Debug.Log("SHIP: Enter state IDLE");
            _playerRotation.SetRotationBlock(true);
            _playerMovement.SetMovementBlock(true);
        }
    }
}
