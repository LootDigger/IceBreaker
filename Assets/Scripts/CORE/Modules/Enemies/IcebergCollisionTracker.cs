using System;
using CORE.Modules.Player;
using CORE.Modules.Player.SM;
using Core.Procedural.PoolManager;
using CORE.Systems.PlayerSystem;
using Patterns.AbstractStateMachine;
using Patterns.Command;
using Patterns.ServiceLocator;
using UnityEngine;

namespace CORE.Systems.Enemies
{
    public class IcebergCollisionTracker : MonoBehaviour
    {
        private PoolManager _poolManager;
        private StateMachine _shipStateMachine;
        
        void Start()
        {
            _poolManager = ServiceLocator.GetService<PoolManager>();
            _shipStateMachine = ServiceLocator.GetService<ShipStateMachine>();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Player>() == null) { return; }
            _shipStateMachine.SetState<SHIP_TakeDamageState>();
            _poolManager.Destroy(gameObject);
        }
    }
}
