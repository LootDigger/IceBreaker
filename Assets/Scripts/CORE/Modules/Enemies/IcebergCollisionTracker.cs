using System;
using CORE.GameStates;
using CORE.Modules.Player;
using CORE.Modules.Player.SM;
using Core.Procedural.Pooling;
using Patterns.AbstractStateMachine;
using Patterns.ServiceLocator;
using UnityEngine;
using UnityEngine.Events;

namespace CORE.Systems.Enemies
{
    public class IcebergCollisionTracker : MonoBehaviour
    {
        [SerializeField] private Collider _collder;
        
        private StateMachine _shipStateMachine;

        public UnityEvent OnPlayerCollision;
        
        void Start()
        {
            _shipStateMachine = ServiceLocator.GetService<ShipStateMachine>();
        }

        private void OnEnable()
        {
            ResetColliderActivity();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Player>() == null) { return; }
            OnPlayerCollision.Invoke();
            SetColliderActivity(false);

            _shipStateMachine.SetState<SHIP_TakeDamageState>();
        }

        private void ResetColliderActivity()
        {
            SetColliderActivity(true);
        }
        
        private void SetColliderActivity(bool isActive) => _collder.enabled = isActive;
    }
}
