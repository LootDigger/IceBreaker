using CORE.Modules.Player;
using CORE.Modules.Player.SM;
using Core.Procedural.PoolManager;
using Patterns.AbstractStateMachine;
using Patterns.ServiceLocator;
using UnityEngine;
using UnityEngine.Events;

namespace CORE.Systems.Enemies
{
    public class IcebergCollisionTracker : MonoBehaviour
    {
        private PoolManager _poolManager;
        private StateMachine _shipStateMachine;

        public UnityEvent OnPlayerCollision;
        
        void Start()
        {
            _poolManager = ServiceLocator.GetService<PoolManager>();
            _shipStateMachine = ServiceLocator.GetService<ShipStateMachine>();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Player>() == null) { return; }
            OnPlayerCollision.Invoke();
            _shipStateMachine.SetState<SHIP_TakeDamageState>();
           // _poolManager.Destroy(gameObject);
        }
    }
}
