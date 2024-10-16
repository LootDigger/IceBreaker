using System.Collections.Generic;
using Core.Procedural.PoolManager;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace CORE.Systems.IceSpawnSystem
{
  public class IceSpawner : MonoBehaviour
  {
    [SerializeField]
    private IceParticleData _iceParticleContainer;
    private List<Transform> _spawnedParticlesInstances;
    
    private PoolManager _poolManager;
  
    private void Awake()
    {
      _poolManager = PoolManager._instance;
      _iceParticleContainer.Init();
      _spawnedParticlesInstances = new ();
    }

    public GameObject SpawnRandomIceParticle(Vector3 position, float spawnRadius)
    {
      var particleData = _iceParticleContainer.GetRandomParticle();
      var particlaGO = _poolManager.Instantiate(particleData.prefab);
      
      if (particlaGO != null)
      {
        particlaGO.transform.position = ComposeSpawnPosition(spawnRadius,position);
        particlaGO.transform.rotation = Quaternion.identity;
      }
      particlaGO.GetComponentInChildren<MeshRenderer>().sharedMaterial = particleData.Material;
      _spawnedParticlesInstances.Add(particlaGO.transform);
      return particlaGO;
    }

    public void Despawn(GameObject particle)
    {
      if (_spawnedParticlesInstances.Contains(particle.transform))
      {
        _spawnedParticlesInstances.Remove(particle.transform);
        particle.SetActive(false);
      }
    }
    
    public Transform[] GetAllParticles() => _spawnedParticlesInstances.ToArray();
    
    private Vector3 ComposeSpawnPosition(float spawnRadius, Vector3 initPosition)
    {
      Vector3 spawnPosition = Random.insideUnitSphere * spawnRadius + initPosition;
      spawnPosition.y = 0f;
      return spawnPosition;
    }
  }
}
