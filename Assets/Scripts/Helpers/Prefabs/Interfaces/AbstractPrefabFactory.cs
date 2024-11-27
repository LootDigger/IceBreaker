using Helpers.PrefabFabric;
using UnityEngine;

namespace Helpers.Prefabs.Interfaces
{
    public abstract class AbstractPrefabFactory
    {
        private readonly IPrefabInstantiator _instantiator;
        private readonly IResourceLoader _resourceLoader;
        private readonly AbstractPrefabVariantsProvider _variantsProvider;
        
        protected AbstractPrefabFactory(IPrefabInstantiator instantiator, IResourceLoader loader, AbstractPrefabVariantsProvider provider)
        {
            _instantiator = instantiator;
            _resourceLoader = loader;
            _variantsProvider = provider;
        }
        
        public virtual GameObject Create(Vector3 position, bool shouldBeActive = true)
        {
            return InstantiatePrefab(LoadPrefab(), position,Quaternion.identity,shouldBeActive);
        }

        protected GameObject LoadPrefab()
        {
            return _resourceLoader.LoadResource<GameObject>(_variantsProvider.GetRandomVariant().GetPath());
        }

        protected GameObject InstantiatePrefab(GameObject prefab, Vector3 position, Quaternion rotation, bool shouldBeActive = true)
        {
            return _instantiator.Instantiate(prefab, position,Quaternion.identity,shouldBeActive);
        }
    }
}
