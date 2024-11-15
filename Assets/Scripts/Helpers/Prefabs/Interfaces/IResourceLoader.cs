using UnityEngine;

namespace Helpers.PrefabFabric
{
   public interface IResourceLoader
   {
      public T LoadResource<T>(string path) where T: Object;
   }
}
