using UnityEngine;

namespace CORE.Modules.ProceduralSystem
{
   [CreateAssetMenu(fileName = "ChunkGenerationSettings", menuName = "ScriptableObjects/Resources/Procedural/ChunkGenerationSettings", order = 1)]
   public class ChunkGenerationSettings : ScriptableObject
   {
      [Header("CHUNK SETTINGS")]
      [Tooltip("Distance to player to generate current chunk")]
      [SerializeField] private float _generateDistance;
      [Tooltip("Distance to player to dispose current chunk")]
      [SerializeField] private float _disposeDistance;
      
      //TODO: Would be nice to make this generic
      [Tooltip("General Chunks Count")]
      [SerializeField] private int _chunksCount;
      
      [Header("PARTICLE SETTINGS")]
      [SerializeField] private int _chunkParticlesCount;
      [SerializeField] private float _particleSpawnRadius;
      
      public float GenerateDistance => _generateDistance;
      public float DisposeDistance => _disposeDistance;
      public int ChunksCount => _chunksCount;
      public int ChunkParticlesCount => _chunkParticlesCount;
      public float ParticleSpawnRadius => _particleSpawnRadius;
      public int GlobalParticlesCount => _chunkParticlesCount * _chunksCount;
   }
}
