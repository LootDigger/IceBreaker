using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Procedural.PoolManager
{
    public class PoolManager : MonoBehaviour
    {
        [SerializeField]
        private List<PoolRequest> _poolRequests = new List<PoolRequest>();
        
        private Dictionary<int,List<GameObject>> pools = new();

        public static PoolManager _instance;

        private void Awake()
        {
            _instance = this;
            InitializePoolRequests();
        }

        private void CreatePool(GameObject prefab, int poolSize)
        {
            List<GameObject> pool = new();
            GameObject poolParent = new GameObject(prefab.name + "_POOL");
            poolParent.transform.position = new Vector3(1000, 1000, 1000);
            for (int i = 0; i < poolSize; i++)
            {
                pool.Add(InstantiatePoolGameObject(prefab,poolParent.transform));
            }
            pools.Add(prefab.GetHashCode(), pool);
        }

        private void InitializePoolRequests()
        {
            for (int i = 0; i < _poolRequests.Count; i++)
            {
                var request = _poolRequests[i];
                CreatePool(request.prefab, request.poolSize);
            }
        }

        public GameObject Instantiate(GameObject prefab)
        {
            if (!IsPoolExists(prefab))
            {
                Debug.LogError("Pool for object " + prefab.name+" isn't registered. Return NULL");
                return null;
            }

            if (IsPoolEmpty(pools[prefab.GetHashCode()]))
            {
                Debug.LogWarning("Pool for object " + prefab.name + " is empty. Return new instance");
                return ReturnNewPoolObject(prefab);
            }

            GameObject result = TryGetGameObjectFromPool(prefab);
            if (result == null)
            { 
                Debug.LogWarning("No free gameObjects in pool " + prefab.name + ". Return new instance");
               return ReturnNewPoolObject(prefab);
            }

            return result;
        }

        public void Destroy(GameObject poolObject)
        {
            poolObject.SetActive(false);
            poolObject.transform.localPosition = Vector3.zero;
        }

        private GameObject InstantiatePoolGameObject(GameObject prefab)
        {
            var instance = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            instance.SetActive(false);
            return instance;
        }
        
        private GameObject InstantiatePoolGameObject(GameObject prefab, Transform parent)
        {
            var instance = Instantiate(prefab, Vector3.zero, Quaternion.identity,parent);
            instance.SetActive(false);
            return instance;
        }

        private GameObject UnpackPoolObject(GameObject poolObject)
        {
            poolObject.SetActive(true);
            return poolObject;
        }

        private GameObject TryGetGameObjectFromPool(GameObject prefab)
        {
            var instances = pools[prefab.GetHashCode()];
            GameObject result = null;
            for (int i = 0; i < instances.Count; i++)
            {
                if (!instances[i].activeSelf)
                {
                    result = UnpackPoolObject(instances[i].gameObject);
                    return result;
                }
            }
            return null;
        }

        private GameObject ReturnNewPoolObject(GameObject prefab)
        {
            var go = UnpackPoolObject(InstantiatePoolGameObject(prefab));
            pools[prefab.GetHashCode()].Add(go);
            return go;
        }
        
        private bool IsPoolEmpty(List<GameObject> instances) => instances.Count == 0;
        
        private bool IsPoolExists(GameObject prefab) => pools.ContainsKey(prefab.GetHashCode());
    }

    [System.Serializable]
    public struct PoolRequest
    {
        public GameObject prefab;
        public int poolSize;
    }
}
