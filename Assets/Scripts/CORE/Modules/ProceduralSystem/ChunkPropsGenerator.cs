using System;
using System.Collections.Generic;
using CORE.Systems.IceSpawnSystem;
using CORE.Systems.IcebergSpawnerSystem;
using Patterns.ServiceLocator;
using UnityEngine;

namespace CORE.Systems.ProceduralSystem
{
    public class ChunkPropsGenerator : MonoBehaviour
    {
        [SerializeField]
        private int _particlesCount;
        [SerializeField]
        private int _spawnRadius;
        
        private IceSpawner _iceParticlesSpawner;
        private List<GameObject> _chunkParticles = new();
        private bool _arePropsChunkGenerated = false;
        
        public static Action onChunkGenerated;
        public static Action onChunkDisposed;
        
        private void Awake()
        {
            _iceParticlesSpawner = ServiceLocator.GetService<IceSpawner>();
        }

        public void Generate()
        {   
            if(_arePropsChunkGenerated) { return; }

            SpawnParticles();
            _arePropsChunkGenerated = true;
            onChunkGenerated.Invoke();
        }

        public void Dispose()
        {
            if(!_arePropsChunkGenerated) { return; }

            DespawnParticles();
            _arePropsChunkGenerated = false; 
            onChunkDisposed.Invoke();
        }
        
        private void SpawnParticles()
        {
            for (int i = 0; i < _particlesCount; i++)
            {
                _chunkParticles.Add(_iceParticlesSpawner.SpawnRandomIceParticle(transform.position, _spawnRadius));
            }
        }
        
        private void DespawnParticles()
        {
            if (_chunkParticles == null) { return; }
            foreach (var particle in _chunkParticles)
            {
                _iceParticlesSpawner.Despawn(particle);
            }
            _chunkParticles.Clear();
        }
    }
}
