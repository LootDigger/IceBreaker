using System.Collections.Generic;
using Core.Procedural.PoolManager;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core
{
  public class IceSpawner : MonoBehaviour
  {
    [SerializeField]
    private Material[] particleMaterialsInstances;
    [SerializeField]
    private GameObject[] particlePrefabVariations;
    [SerializeField]
    private List<Transform> _spawnedParticlesInstances;
  
    private void Awake()
    {
      InstantiateMaterials();
      _spawnedParticlesInstances = new ();
    }

    private void OnDestroy()
    {
      foreach (var material in particleMaterialsInstances)
      {
        Destroy(material);
      }
    }

    public GameObject SpawnRandomIcePrefab(Vector3 position, float spawnRadius)
    {
      Vector3 spawnPosition = Random.insideUnitSphere * spawnRadius + position;
      spawnPosition.y = 0f;
      int prefabIndex = Random.Range(0, particlePrefabVariations.Length);

      var particle = PoolManager._instance.Instantiate(particlePrefabVariations[prefabIndex]);
      if (particle != null)
      {
        particle.transform.position = spawnPosition;
        particle.transform.rotation = Quaternion.identity;
      }
      particle.GetComponentInChildren<MeshRenderer>().sharedMaterial = particleMaterialsInstances[prefabIndex];
      _spawnedParticlesInstances.Add(particle.transform);
      return particle;
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

    private void InstantiateMaterials()
    {
      for (int prefabIndex = 0; prefabIndex < particlePrefabVariations.Length; prefabIndex++)
      {
        particleMaterialsInstances[prefabIndex] = Instantiate(particleMaterialsInstances[prefabIndex]);
      }
    }
  }
}
