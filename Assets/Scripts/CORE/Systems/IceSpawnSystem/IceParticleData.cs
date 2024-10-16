using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace CORE.Systems.IceSpawnSystem
{
    [CreateAssetMenu(fileName = "IceSettings", menuName = "ScriptableObjects/ICESpawn/Data")]
    public class IceParticleData : ScriptableObject
    {
        public struct IceParticle
        {
            public GameObject prefab;
            public Material Material;

            public IceParticle(GameObject prefab, Material material)
            {
                this.prefab = prefab;
                Material = material;
            }
        }
        
        
        [SerializeField]
        private Material[] _particleMaterials;
        [SerializeField]
        private GameObject[] _particlePrefabVariations;
        
        private Material[] _materialsInstances;
        
        public void OnDestroy()
        {
            foreach (var material in _materialsInstances)
            {
                Destroy(material);
            }
        }
        
        public void Init()
        {
            InstantiateMaterials();
        }

        public IceParticle GetRandomParticle()
        {
            int index = Random.Range(0, _particlePrefabVariations.Length);
            return new IceParticle(GetPrefabByIndex(index),GetMaterialByIndex(index));
        }
        
        private GameObject GetPrefabByIndex(int index)
        {
            return _particlePrefabVariations[index];
        }
        
        private Material GetMaterialByIndex(int index)
        {
            return _materialsInstances[index];
        }
        
        private void InstantiateMaterials()
        {
            _materialsInstances = new Material[_particlePrefabVariations.Length];
            
            for (int index = 0; index < _particlePrefabVariations.Length; index++)
            {
                _materialsInstances[index] = Instantiate(_particleMaterials[index]);
            }
        }
    }
}
