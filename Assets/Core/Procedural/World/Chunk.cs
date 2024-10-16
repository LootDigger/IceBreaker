using System;
using System.Collections.Generic;
using Core.ShipControls;
using CORE.Systems.IceSpawnSystem;
using CORE.Systems.PlayerSystem;
using Patterns.ServiceLocator;
using UnityEngine;

namespace Core.Procedural.World
{
    public class Chunk : MonoBehaviour
    {
        [SerializeField]
        private float _generateDistance;
        [SerializeField]
        private float _disposeDistance;
        [SerializeField]
        private int _particlesCount;
        [SerializeField]
        private int _spawnRadius;
        
        private IceSpawner _spawner;
        private List<GameObject> _chunkParticles = new();
        private GameObject _player;
        private bool _isChunkGenerated = false;

        public static Action onChunkGenerated;
        public static Action onChunkDisposed;
        
        void Awake()
        {
            _spawner = ServiceLocator.GetService<IceSpawner>();
            _player = ServiceLocator.GetService<Player>().gameObject;
        }

        private void Update()
        {
            UpdateChunk();
        }

        private void UpdateChunk()
        {
            if (CalculateDistanceToPlayer() < _generateDistance && !_isChunkGenerated)
            {
                Generate();
            }
            else if (CalculateDistanceToPlayer() > _disposeDistance && _isChunkGenerated)
            {
                Dispose();
            }
        }

        private float CalculateDistanceToPlayer()
        {
            return Vector3.Distance(_player.transform.position, transform.position);
        }

        private void SpawnParticles()
        {
            for (int i = 0; i < _particlesCount; i++)
            {
                _chunkParticles.Add(_spawner.SpawnRandomIceParticle(transform.position, _spawnRadius));
            }
        }
        
        private void DespawnParticles()
        {
            if (_chunkParticles == null) { return; }
            foreach (var particle in _chunkParticles)
            {
                _spawner.Despawn(particle);
            }
            _chunkParticles.Clear();
        }
        
        private void Generate()
        {
            if(_isChunkGenerated) { return; }
            SpawnParticles();
            _isChunkGenerated = true;
            onChunkGenerated.Invoke();
        }

        private void Dispose()
        {
            if(!_isChunkGenerated) { return; }
            DespawnParticles();
            _isChunkGenerated = false; 
            onChunkDisposed.Invoke();
        }
    }
}
