using System;
using Patterns.ServiceLocator;
using UnityEngine;

namespace CORE.Systems.PlayerSystem
{
    public class Player : MonoBehaviour
    {
        private void Awake()
        {
            ServiceLocator.RegisterService(this);
        }
    }
}
