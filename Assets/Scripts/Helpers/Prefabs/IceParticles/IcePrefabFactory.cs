using System;
using Helpers.PrefabFabric;
using Helpers.Prefabs.Interfaces;
using UnityEngine;

namespace Helpers.Prefabs
{
    public class IcePrefabFactory : AbstractPrefabFactory
    {
        private readonly IMaterialInstanceProvider _materialInstanceProvider;
        private readonly IFactoryObjectsKeeper<Transform> _factoryObjectsKeeper;
        
        public IcePrefabFactory(
            IPrefabInstantiator instantiator,
            IResourceLoader loader,
            AbstractPrefabVariantsProvider provider,
            IMaterialInstanceProvider materialProvider,
            IFactoryObjectsKeeper<Transform> factoryObjectsKeeper)
            : base(instantiator, loader, provider)
        {
            _materialInstanceProvider = materialProvider;
            _factoryObjectsKeeper = factoryObjectsKeeper;
        }
        
        public override GameObject Create(Vector3 position)
        {
            try
            {
                GameObject prefab = LoadPrefab();
                GameObject objectInstance = InstantiatePrefab(prefab, position, Quaternion.identity);
                objectInstance.GetComponentInChildren<MeshRenderer>().sharedMaterial = _materialInstanceProvider.GetMaterialInstance(prefab);
                _factoryObjectsKeeper.AddObject(objectInstance.transform);
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
