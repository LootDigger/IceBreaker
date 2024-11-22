using System;
using System.Collections;
using System.Collections.Generic;
using Helpers.Prefabs;
using Patterns.ServiceLocator;
using UnityEngine;

public class IceObjectKeeperWrapperDebug : MonoBehaviour
{
   public Transform[] transforms;

   IceFactoryObjectsKeeper iceFactoryObjectsKeeper;
   private void Start()
   {
      iceFactoryObjectsKeeper = ServiceLocator.GetService<IceFactoryObjectsKeeper>();
   }

   private void Update()
   {
      transforms = iceFactoryObjectsKeeper.GetObjects();
   }
}
