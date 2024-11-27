using System.Collections.Generic;
using UnityEngine;

namespace Helpers.Prefabs.Iceberg
{
    // Would be nice to move this to array to optimise GetObjects Method as I did it for iceObjectsKeeper
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
