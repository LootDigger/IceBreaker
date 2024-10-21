using System;
using System.Collections.Generic;
using Core.Procedural.PoolManager;
using Patterns.ServiceLocator;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace CORE.Systems.IceSpawnSystem
{
  public class IceSpawner : MonoBehaviour
  {
    [SerializeField]
    private PrefabVariantsData _prefabVariantsContainer;
    private List<Transform> _spawnedParticlesInstances;
    
    private PoolManager _poolManager;
  
    private void Awake()
    {
      ServiceLocator.RegisterService(this);
      _prefabVariantsContainer.Init();
      _spawnedParticlesInstances = new ();
    }

    private void Start()
    {
      _poolManager = ServiceLocator.GetService<PoolManager>();
    }

    public GameObject SpawnRandomIceParticle(Vector3 position, float spawnRadius)
    {
      var particleData = _prefabVariantsContainer.GetRandomVariant();
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
