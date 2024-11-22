using UnityEngine;

namespace Helpers.Prefabs
{
    public class IceFactoryObjectsKeeper : IFactoryObjectsKeeper<Transform>
    {
        private readonly Transform[] _spawnedIceObjects;
        private readonly int _maxCount = 1000;
        
        private int _index = 0;
        
        public IceFactoryObjectsKeeper()
        {
            _spawnedIceObjects = new Transform[_maxCount];
        }
        
        public void AddObject(Transform obj)
        {
            AssignValue(obj);
        }

        public void RemoveObject(Transform obj)
        {
        }

        public Transform[] GetObjects()
        {
            return _spawnedIceObjects;
        }

        
        private void AssignValue(Transform obj)
        {
            _spawnedIceObjects[_index] = obj;
            _index++;
        }
    }
}

