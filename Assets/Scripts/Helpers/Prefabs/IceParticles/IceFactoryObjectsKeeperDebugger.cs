using System.Collections;
using System.Collections.Generic;
using Helpers.Prefabs;
using Patterns.ServiceLocator;
using UnityEngine;

public class IceFactoryObjectsKeeperDebugger : MonoBehaviour
{
    [SerializeField]
    private Transform[] instantiatedObjects;
    
    void Start()
    {
        instantiatedObjects = ServiceLocator.GetService<IceFactoryObjectsKeeper>().GetObjects();
    }
}
