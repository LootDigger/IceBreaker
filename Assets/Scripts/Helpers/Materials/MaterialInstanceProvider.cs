using System.Collections.Generic;
using UnityEngine;

namespace Helpers.Materials
{
    public class MaterialInstanceProvider : IMaterialInstanceProvider
    {
        private Dictionary<GameObject,Material> _prefabMaterialInstances = new();
        private MaterialInstantiator _instantiator = new();
    
        public Material GetMaterialInstance(GameObject prefab)
        {
            if (!_prefabMaterialInstances.ContainsKey(prefab))
            {
                ConstructMaterialInstance(prefab);
            }
            return _prefabMaterialInstances[prefab];
        }

        private void ConstructMaterialInstance(GameObject prefab)
        {
            _prefabMaterialInstances.Add(prefab,_instantiator.InstantiateMaterial(prefab));
        }
    }
}
