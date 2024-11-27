using CORE.Modules.ProceduralSystem;
using Patterns.ServiceLocator;
using UnityEngine;

namespace CORE.Modules.Procedural
{
    [RequireComponent(typeof(ChunkParticlesGenerator))]
    public class Chunk : MonoBehaviour
    {
        [SerializeField] 
        private ChunkGenerationSettings _generationSettings;
        [SerializeField]
        private ChunkParticlesGenerator _particlesGenerator;
        [SerializeField]
        private ChunkEnemyGenerator _enemyGenerator;
        
        private GameObject _player;
        private bool _isGenerated;
        private bool _isInitialized;
        private Vector3 _chunkPosition;

        public ChunkEnemyGenerator EnemyGenerator => _enemyGenerator;
        
        public void Init(GameObject playerGORef)
        {
            _player = playerGORef;
            // _player = ServiceLocator.GetService<Player.Player>().gameObject;
            _chunkPosition = transform.position;
            _isInitialized = true;
        }
        
        public void UpdateChunkRoutine()
        {
            if(!_isInitialized) return;
            
            float distance = CalculateDistanceToPlayer();
            if (!_isGenerated && distance < _generationSettings.GenerateDistance)
            {
                _isGenerated = Generate();
            }
            else if (_isGenerated && distance > _generationSettings.DisposeDistance)
            {
                _isGenerated = Dispose();
            }
        }

        private float CalculateDistanceToPlayer()
        {
            return Vector3.Distance(_player.transform.position, _chunkPosition);
        }

        private bool Generate()
        {
            _particlesGenerator.Generate();
            _enemyGenerator.Generate();
            return true;
        }
        
        private bool Dispose()
        {       
            _particlesGenerator.Dispose();
            _enemyGenerator.Dispose();
            return false;
        }
    }
}
