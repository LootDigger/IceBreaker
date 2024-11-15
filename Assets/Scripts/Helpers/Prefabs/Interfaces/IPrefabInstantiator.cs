using UnityEngine;

namespace Helpers.PrefabFabric
{
    public interface IPrefabInstantiator 
    {
        public GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation);
    }
}
