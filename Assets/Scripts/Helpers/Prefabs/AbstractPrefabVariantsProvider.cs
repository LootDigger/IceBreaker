using System.Collections.Generic;
using Helpers.Prefabs.Interfaces;
using Helpers.ResourceManagement;
using UnityEngine;

namespace Helpers.Prefabs
{
    public abstract class AbstractPrefabVariantsProvider
    {
        protected readonly List<IResourcePathProvider> VariantsPathProviders = new();

        public void AddVariant(IResourcePathProvider variantsProvider)
        {
            VariantsPathProviders.Add(variantsProvider);
        }

        public IResourcePathProvider GetVariantByIndex(int index)
        {
            return VariantsPathProviders[index];
        }

        public IResourcePathProvider GetRandomVariant()
        {
            return VariantsPathProviders[Random.Range(0, VariantsPathProviders.Count)];
        }
    }
}
