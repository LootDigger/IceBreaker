using System;
using Patterns.ServiceLocator;
using UnityEngine;

namespace CORE.Modules.PlayerSystem.SM
{
    public class ShipSMInitializer : MonoBehaviour
    {
        private void Start()
        {
            ServiceLocator.RegisterService(new ShipStateMachine());
        }
    }
}
