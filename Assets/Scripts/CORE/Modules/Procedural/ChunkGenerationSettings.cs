using System.Collections.Generic;
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
      
      [Header("PARTICLE SETTINGS")]
      [SerializeField] private int _particlesCount;
      [SerializeField] private float _particleSpawnRadius;

      [SerializeField] private List<GameObject> _particleVariants;
      
   
      public int ParticlesCount => _particlesCount;
      public float ParticleSpawnRadius => _particleSpawnRadius;
      public float GenerateDistance => _generateDistance;
      public float DisposeDistance => _disposeDistance;
      
   }
}
