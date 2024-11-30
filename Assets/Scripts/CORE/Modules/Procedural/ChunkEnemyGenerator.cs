using System.Collections.Generic;
using Core.Procedural.Pooling;
using Helpers.Prefabs;
using Helpers.Prefabs.Iceberg;
using Helpers.Prefabs.Interfaces;
using Patterns.ServiceLocator;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CORE.Modules.ProceduralSystem
{
    public class ChunkEnemyGenerator : MonoBehaviour
    {
        [SerializeField]
        private float _spawnRadius = 10f;
        [SerializeField]
        private Collider _nonSpawnArea;
        
        private PoolManager _poolManager;
        private AbstractPrefabFactory _icebergPrefabFactory;
        private readonly List<GameObject> _generatedIcebergs = new();
        private int _enemyCount;

        public void Init(PoolManager poolManRef, IcebergPrefabFactory icebergFactoryRef, int enemyCount)
        {
           _poolManager = poolManRef;
           _icebergPrefabFactory = icebergFactoryRef;
           _enemyCount = enemyCount;
        }
        
        private void SpawnIcebergs()
        {
            for (int i = 0; i < _enemyCount; i++)
            {
                Vector3 position = ComposeSpawnPosition(_spawnRadius, transform.position);
                while (IsSpawnPosInNonSpawnArea(_nonSpawnArea,position))
                {
                    position = ComposeSpawnPosition(_spawnRadius, transform.position);
                }
                _generatedIcebergs.Add(_icebergPrefabFactory.Create(position));
            }
        }

        private void DisposeIcebergs()
        {
            if (_generatedIcebergs == null) { return; }
            foreach (var particle in _generatedIcebergs)
            {
                _poolManager.Destroy(particle);
            }
            _generatedIcebergs.Clear();
        }
        
        private Vector3 ComposeSpawnPosition(float spawnRadius, Vector3 initPosition)
        {
            Vector3 spawnPosition = Random.insideUnitSphere * spawnRadius + initPosition;
            spawnPosition.y = 0f;
            return spawnPosition;
        }

        private bool IsSpawnPosInNonSpawnArea(Collider nonSpawnArea, Vector3 point)
        {
            return nonSpawnArea.bounds.Contains(point);
        }
        
        public void Generate() => SpawnIcebergs();

        public void Dispose() => DisposeIcebergs();
        
        public void InjectNonSpawnCollider(Collider nonSpawnAreaCollider) => _nonSpawnArea = nonSpawnAreaCollider;
    }
}
