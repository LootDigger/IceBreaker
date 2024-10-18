using Core.Procedural.PoolManager;
using CORE.Systems.PlayerSystem;
using CORE.Systems.PlayerSystem.SM.Commands;
using Patterns.Command;
using Patterns.ServiceLocator;
using UnityEngine;

namespace CORE.Systems.Enemies
{
    public class IcebergCollisionTracker : MonoBehaviour
    {
        private PoolManager _poolManager;

        void Start()
        {
            _poolManager = ServiceLocator.GetService<PoolManager>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Player>() == null) { return; }
            CommandExecuter.ExecuteCommand(new SetTakeDamageStateCommand());
            
            // TEMP
            _poolManager.Destroy(gameObject);
        }
    }
}
