using UnityEngine;

namespace Helpers.Prefabs
{
   public interface IFactoryObjectsKeeper<T> where T: UnityEngine.Object
   {
      public void AddObject(T obj);
      public void RemoveObject(T obj);
      public T[] GetObjects();
   }
}
