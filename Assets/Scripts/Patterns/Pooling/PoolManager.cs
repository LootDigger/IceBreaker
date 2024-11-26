using System.Collections.Generic;
using Patterns.ServiceLocator;
using UnityEngine;

namespace Core.Procedural.Pooling
{
    public class PoolManager : MonoBehaviour
    {
        public struct Pool
        {
            public Transform poolParentGO;
            public List<GameObject> pooledObjects;

            public Pool(Transform parentGo, List<GameObject> pooledObjectObjects)
            {
                poolParentGO = parentGo;
                pooledObjects = pooledObjectObjects;
            }
        }
        
        [SerializeField]
        private List<PoolRequest> _poolRequests = new();
        
        private Dictionary<int,Pool> _pools = new();
        
        private void Awake()
        {
            ServiceLocator.RegisterService(this);
            InitializePoolRequests();
            DontDestroyOnLoad(this);
        }

        public void CreatePool(GameObject prefab, int poolSize)
        {
            if (IsPoolExists(prefab))
            {
                Debug.LogWarning("Pool of gameObject " +prefab.name+ " already exists!");
                return;
            }
            
            List<GameObject> poolObjects = new();
            GameObject poolParent = new(prefab.name + "_POOL");
            poolParent.transform.SetParent(transform);
            poolParent.transform.position = new Vector3(1000, 1000, 1000);
            for (int i = 0; i < poolSize; i++)
            {
                poolObjects.Add(InstantiatePoolGameObject(prefab,poolParent.transform));
            }
            
            Pool pool = new Pool(poolParent.transform,poolObjects);
            _pools.Add(prefab.GetHashCode(), pool);
        }

        private void InitializePoolRequests()
        {
            for (int i = 0; i < _poolRequests.Count; i++)
            {
                var request = _poolRequests[i];
                CreatePool(request.prefab, request.poolSize);
            }
        }

        public GameObject Instantiate(GameObject prefab, bool isActive = true)
        {
            if (!IsPoolExists(prefab))
            {
                Debug.LogWarning("Pool for object " + prefab.name+" isn't registered. Create new Pool of size 1 and try again");
                CreatePool(prefab,1);
                return Instantiate(prefab,isActive);
            }

            if (IsPoolEmpty(_pools[prefab.GetHashCode()].pooledObjects))
            {
                Debug.LogWarning("Pool for object " + prefab.name + " is empty. Return new instance");
                return ReturnNewPoolObject(prefab,isActive);
            }

            GameObject result = TryGetGameObjectFromPool(prefab,isActive);
            if (result == null)
            { 
                Debug.LogWarning("No free gameObjects in pool " + prefab.name + ". Return new instance");
                return ReturnNewPoolObject(prefab,isActive);
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

        private GameObject TryGetGameObjectFromPool(GameObject prefab, bool isActive = true)
        {
            var instances = _pools[prefab.GetHashCode()].pooledObjects;
            GameObject result = null;
            for (int i = 0; i < instances.Count; i++)
            {
                if (!instances[i].activeSelf)
                {
                    if (isActive)
                    {
                        result = UnpackPoolObject(instances[i].gameObject);
                    }
                    return result;
                }
            }
            return null;
        }

        private GameObject ReturnNewPoolObject(GameObject prefab, bool shouldBeActive = true)
        {
            Pool pool = _pools[prefab.GetHashCode()];
            var gameObject = InstantiatePoolGameObject(prefab, pool.poolParentGO);
            if (shouldBeActive)
            {
                gameObject = UnpackPoolObject(gameObject);
            }
            pool.pooledObjects.Add(gameObject);
            return gameObject;
        }
        
        private bool IsPoolEmpty(List<GameObject> instances) => instances.Count == 0;
        
        private bool IsPoolExists(GameObject prefab) => _pools.ContainsKey(prefab.GetHashCode());
    }

    [System.Serializable]
    public struct PoolRequest
    {
        public GameObject prefab;
        public int poolSize;
    }
}
