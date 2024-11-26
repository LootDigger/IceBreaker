using Core.Procedural.Pooling;
using Patterns.ServiceLocator;
using UnityEngine;

namespace Helpers.PrefabFabric
{
    public class PrefabInstantiator : IPrefabInstantiator
    {
        private PoolManager _poolManager;

        public PrefabInstantiator()
        {
            _poolManager = ServiceLocator.GetService<PoolManager>();
        }

        public GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation, bool shouldBeActive = true)
        {
            GameObject gameObject = _poolManager.Instantiate(prefab,shouldBeActive);
            if (gameObject == null) return null;
            
            gameObject.transform.position = position;
            gameObject.transform.rotation = rotation;
            return gameObject;
        }
    }
}
