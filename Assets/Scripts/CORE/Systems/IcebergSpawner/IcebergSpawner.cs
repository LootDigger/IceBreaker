using Core.Procedural.PoolManager;
using CORE.Systems.IceSpawnSystem;
using Patterns.ServiceLocator;
using UnityEngine;

namespace CORE.Systems.IcebergSpawnerSystem
{
    public class IcebergSpawner : MonoBehaviour
    { 
        [SerializeField]
        private PrefabVariantsData _prefabVariantsData;
        
        private PoolManager _poolManager;
        private void Awake()
        {
            ServiceLocator.RegisterService(this);
            _prefabVariantsData.Init();
        }

        void Start()
        {
            _poolManager = ServiceLocator.GetService<PoolManager>();
        }

        public GameObject SpawnIceberg(Vector3 position, float radius)
        {
            var variant = _prefabVariantsData.GetRandomVariant();
            var go = _poolManager.Instantiate(variant.prefab);
            if (go != null)
            {
                go.transform.position = ComposeSpawnPosition(position,radius);
                go.transform.rotation = Quaternion.identity;
            }
            go.GetComponentInChildren<MeshRenderer>().sharedMaterial = variant.Material;
            return go;
        }
       
        public void Despawn(GameObject gameObject)
        {
            _poolManager.Destroy(gameObject);
        }
       
        private Vector3 ComposeSpawnPosition(Vector3 initPosition,float spawnRadius)
        {
            Vector3 spawnPosition = Random.insideUnitSphere * spawnRadius + initPosition;
            spawnPosition.y = 0f;
            return spawnPosition;
        }
    }
}
