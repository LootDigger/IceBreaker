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

        public ChunkEnemyGenerator EnemyGenerator => _enemyGenerator;
        
        private void Awake()
        {
            _player = ServiceLocator.GetService<Player.Player>().gameObject;
        }

        private void Update() => UpdateChunkRoutine();

        private void UpdateChunkRoutine()
        {
            if (CalculateDistanceToPlayer() < _generationSettings.GenerateDistance && !_isGenerated)
            {
                _isGenerated = Generate();
            }
            else if (CalculateDistanceToPlayer() > _generationSettings.DisposeDistance && _isGenerated)
            {
                _isGenerated = Dispose();
            }
        }

        private float CalculateDistanceToPlayer()
        {
            return Vector3.Distance(_player.transform.position, transform.position);
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
