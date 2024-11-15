using System.Collections.Generic;
using UnityEngine;

namespace Helpers.Prefabs
{
    public class IceFactoryObjectsKeeper : IFactoryObjectsKeeper<Transform>
    {
        private readonly List<Transform> _factoryObjects = new();
        public void AddObject(Transform obj)
        {
            _factoryObjects.Add(obj);
        }

        public void RemoveObject(Transform obj)
        {
            _factoryObjects.Remove(obj);
        }

        public Transform[] GetObjects()
        {
            return _factoryObjects.ToArray();
        }
    }
}

