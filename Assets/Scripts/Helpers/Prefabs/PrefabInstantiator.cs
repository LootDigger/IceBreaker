using Core.Procedural.PoolManager;
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

        public GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            GameObject gameObject = _poolManager.Instantiate(prefab);
            gameObject.transform.position = position;
            gameObject.transform.rotation = rotation;
            return gameObject;
        }
    }
}
