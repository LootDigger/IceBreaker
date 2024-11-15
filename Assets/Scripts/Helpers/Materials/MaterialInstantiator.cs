using UnityEngine;

namespace Helpers.Materials
{
    public class MaterialInstantiator : IMaterialInstantiator
    {
        public Material InstantiateMaterial(GameObject prefab)
        {
            var material = prefab.GetComponentInChildren<MeshRenderer>().sharedMaterial;
            return Material.Instantiate(material);
        }
    }
}
