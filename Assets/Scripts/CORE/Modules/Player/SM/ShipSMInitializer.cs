using Patterns.ServiceLocator;
using UnityEngine;

namespace CORE.Modules.Player.SM
{
    public class ShipSMInitializer : MonoBehaviour
    {
        private void Start()
        {
            Debug.Log("### Ship SM Initializer");
            ServiceLocator.RegisterService(new ShipStateMachine());
        }
    }
}
