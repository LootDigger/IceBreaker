using System.Collections.Generic;
using UnityEngine;

namespace Helpers.Prefabs.Iceberg
{
    public class IcebergFactoryObjectsKeeper : IFactoryObjectsKeeper<GameObject>
    {
        private readonly List<GameObject> _factoryObjects = new();

        public void AddObject(GameObject obj)
        {
            _factoryObjects.Add(obj);
        }

        public void RemoveObject(GameObject obj)
        {
            _factoryObjects.Remove(obj);
        }

        public GameObject[] GetObjects()
        {
            return _factoryObjects.ToArray();
        }
    }
}
