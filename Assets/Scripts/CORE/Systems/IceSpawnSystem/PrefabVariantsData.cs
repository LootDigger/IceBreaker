using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace CORE.Systems.IceSpawnSystem
{
    [CreateAssetMenu(fileName = "PrefabVariantsContainer", menuName = "ScriptableObjects/Prefabs/PrefabVariantRandomizer")]
    public class PrefabVariantsData : ScriptableObject
    {
        public struct PrefabVariant
        {
            public GameObject prefab;
            public Material Material;

            public PrefabVariant(GameObject prefab, Material material)
            {
                this.prefab = prefab;
                Material = material;
            }
        }
        
        
        [FormerlySerializedAs("_particleMaterials")] [SerializeField]
        private Material[] _materials;
        [FormerlySerializedAs("_particlePrefabVariations")] [SerializeField]
        private GameObject[] _prefabVariations;
        
        private Material[] _materialsInstances;

        private void Awake()
        {
            InstantiateMaterials();

        }

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

        public PrefabVariant GetRandomVariant()
        {
            int index = Random.Range(0, _prefabVariations.Length);
            return new PrefabVariant(GetPrefabByIndex(index),GetMaterialByIndex(index));
        }
        
        private GameObject GetPrefabByIndex(int index)
        {
            return _prefabVariations[index];
        }
        
        private Material GetMaterialByIndex(int index)
        {
            return _materialsInstances[index];
        }
        
        private void InstantiateMaterials()
        {
            _materialsInstances = new Material[_prefabVariations.Length];
            
            for (int index = 0; index < _prefabVariations.Length; index++)
            {
                _materialsInstances[index] = Instantiate(_materials[index]);
            }
        }
    }
}
