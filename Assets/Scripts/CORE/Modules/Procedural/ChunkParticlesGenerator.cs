using System.Collections.Generic;
using Core.Procedural.PoolManager;
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
        private IFactoryObjectsKeeper<Transform> _factoryObjectsKeeper;
        private readonly List<GameObject> _generatedParticles = new();
        
        private void Awake() => GetServices();
        
        private void GetServices()
        {
            _poolManager = ServiceLocator.GetService<PoolManager>();
            _icePrefabFactory = ServiceLocator.GetService<IcePrefabFactory>();
            _factoryObjectsKeeper = ServiceLocator.GetService<IceFactoryObjectsKeeper>();
        }
        
        private void SpawnParticles()
        {
            for (int i = 0; i < _generationSettings.ParticlesCount; i++)
            {
                Vector3 position = ComposeSpawnPosition(_generationSettings.ParticleSpawnRadius, transform.position);
                _generatedParticles.Add(_icePrefabFactory.Create(position));
            }
        }
        
        private void DespawnParticles()
        {
            if (_generatedParticles.IsNullOrEmpty()) {return;}
            for (int i = 0; i < _generatedParticles.Count; i++)
            {
                _factoryObjectsKeeper.RemoveObject(_generatedParticles[i].transform);
                _poolManager.Destroy(_generatedParticles[i]);
            }
            _generatedParticles.Clear();
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
