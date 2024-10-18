using System;
using System.Collections.Generic;
using CORE.Systems.IceSpawnSystem;
using CORE.Systems.IcebergSpawnerSystem;
using Patterns.ServiceLocator;
using UnityEngine;

namespace CORE.Systems.ProceduralSystem
{
    public class ChunkEnemyGenerator : MonoBehaviour
    {
        [SerializeField]
        private int _enemyCount = 1;
        [SerializeField]
        private float _spawnRadius = 10f;
        
        private List<GameObject> _spawnedIcebergs = new();
        private IcebergSpawner _icebergSpawner;
        
        private void Awake()
        {
            _icebergSpawner = ServiceLocator.GetService<IcebergSpawner>();
        }
        
        private GameObject SpawnIceberg()
        {
            return _icebergSpawner.SpawnIceberg(transform.position,_spawnRadius);
        }

        public void Generate()
        {
            for (int i = 0; i < _enemyCount; i++)
            {
                _spawnedIcebergs.Add(SpawnIceberg());
            }
        }

        public void Dispose()
        {
            if (_spawnedIcebergs == null) { return; }
            foreach (var particle in _spawnedIcebergs)
            {
                _icebergSpawner.Despawn(particle);
            }
            _spawnedIcebergs.Clear();
        }
    }
}
