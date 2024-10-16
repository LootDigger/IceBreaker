using System;
using System.Collections.Generic;
using Core.ShipControls;
using UnityEngine;
using Sirenix.OdinInspector;

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
        private bool _isChunkGenerated = false;
        private GameObject _player;
        
        public static Action onChunkGenerated;
        public static Action onChunkDisposed;
        
        void Awake()
        {
            _spawner = FindObjectOfType<IceSpawner>();
            _player = FindObjectOfType<ShipMovementManager>().gameObject;
        }

        private void Update()
        {
            var distance = Vector3.Distance(_player.transform.position, transform.position);
            if (distance < _generateDistance)
            {
                Generate();
            }
            if (distance > _disposeDistance)
            {
                Dispose();
            }
        }

        private void SpawnParticles()
        {
            for (int i = 0; i < _particlesCount; i++)
            {
                _chunkParticles.Add(_spawner.SpawnRandomIcePrefab(transform.position, _spawnRadius));
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
