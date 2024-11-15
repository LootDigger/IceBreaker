using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMaterialInstanceProvider
{
   Material GetMaterialInstance(GameObject prefab);
}
