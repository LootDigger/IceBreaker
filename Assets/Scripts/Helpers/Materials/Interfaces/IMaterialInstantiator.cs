using UnityEngine;

namespace Helpers.Materials
{
    public interface IMaterialInstantiator
    {
        public Material InstantiateMaterial(GameObject prefab);
    }
}
