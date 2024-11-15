using UnityEngine;

namespace Helpers.PrefabFabric
{
    public class PrefabResourceLoader : IResourceLoader
    {
        public T LoadResource<T>(string path) where T : Object
        {
            return Resources.Load<T>(path);
        }
    }
}
