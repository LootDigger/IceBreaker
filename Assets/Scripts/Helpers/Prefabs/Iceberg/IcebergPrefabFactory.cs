using System;
using Helpers.PrefabFabric;
using Helpers.Prefabs.Interfaces;
using UnityEngine;

namespace Helpers.Prefabs
{
    public class IcebergPrefabFactory : AbstractPrefabFactory
    {
        private readonly IMaterialInstanceProvider _materialInstanceProvider;
        private readonly IFactoryObjectsKeeper<GameObject> _objectsKeeper;
        
        public IcebergPrefabFactory(
            IPrefabInstantiator instantiator, 
            IResourceLoader loader, 
            AbstractPrefabVariantsProvider provider,
            IMaterialInstanceProvider materialProvider,
            IFactoryObjectsKeeper<GameObject> objectsKeeper) 
            : base(instantiator, loader, provider)
        {
            _materialInstanceProvider = materialProvider;
            _objectsKeeper = objectsKeeper;
        }
        
        public override GameObject Create(Vector3 position, bool shouldBeActive = true)
        {
            try
            {
                GameObject prefab = LoadPrefab();
                GameObject objectInstance = InstantiatePrefab(prefab, position, Quaternion.identity,shouldBeActive);
                objectInstance.GetComponentInChildren<MeshRenderer>().sharedMaterial = _materialInstanceProvider.GetMaterialInstance(prefab);
                _objectsKeeper.AddObject(objectInstance);
                return objectInstance;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
        }
    }
}
