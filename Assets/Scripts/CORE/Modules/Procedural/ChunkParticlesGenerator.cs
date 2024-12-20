using System.Collections.Generic;
using Core.Procedural.Pooling;
using Helpers.Prefabs;
using Helpers.Prefabs.Interfaces;
using Patterns.ServiceLocator;
using Sirenix.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CORE.Modules.ProceduralSystem
{
    public class ChunkParticlesGenerator : MonoBehaviour
    {
        [SerializeField] 
        private ChunkGenerationSettings _generationSettings;
        
        private PoolManager _poolManager;
        private AbstractPrefabFactory _icePrefabFactory;
        private readonly List<GameObject> _generatedParticles = new();

        public void Init(PoolManager poolManager, IcePrefabFactory icePrefabFactory)
        {
            SetupServices(poolManager,icePrefabFactory);
            WarmupParticles();
        }

        private void SetupServices(PoolManager poolManager, IcePrefabFactory icePrefabFactory)
        {
            _poolManager = poolManager;
            _icePrefabFactory = icePrefabFactory;
        }

        private void WarmupParticles()
        {
            for (int i = 0; i < _generationSettings.ChunkParticlesCount; i++)
            {
                var generatedObject = _icePrefabFactory.Create(Vector3.zero,shouldBeActive:false);
                _generatedParticles.Add(generatedObject);
            }
        }
        
        private void SpawnParticles()
        {
            if (_generatedParticles.IsNullOrEmpty()) { return; }
            for (int i = 0; i < _generatedParticles.Count; i++)
            {
                Vector3 position = ComposeSpawnPosition(_generationSettings.ParticleSpawnRadius, transform.position);
                _generatedParticles[i].transform.position = position;
                _generatedParticles[i].SetActive(true);
            }
        }
        
        private void DespawnParticles()
        {
            if (_generatedParticles.IsNullOrEmpty()) { return; }
            for (int i = 0; i < _generatedParticles.Count; i++)
            {
                _poolManager.Destroy(_generatedParticles[i]);
            }
        }
        
        private Vector3 ComposeSpawnPosition(float spawnRadius, Vector3 initPosition)
        {
            Vector3 spawnPosition = Random.insideUnitSphere * spawnRadius + initPosition;
            spawnPosition.y = 0f;
            return spawnPosition;
        }
        
        public void Generate() => SpawnParticles();

        public void Dispose() => DespawnParticles();
    }
}
