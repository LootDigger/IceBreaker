using Patterns.ServiceLocator;
using UnityEngine;

namespace CORE.Modules.Player.SM
{
    public class ShipSMInitializer : MonoBehaviour
    {
        private void Start()
        {
            ServiceLocator.RegisterService(new ShipStateMachine());
        }
    }
}
