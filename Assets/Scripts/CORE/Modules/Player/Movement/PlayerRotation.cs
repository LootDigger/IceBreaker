using Core.ShipControls;
using Patterns.ServiceLocator;
using UnityEngine;

namespace CORE.Systems.PlayerSystem.Movement
{
    public class PlayerRotation : MonoBehaviour
    {
        private IControlDriver _currentControlDriver = null;
        private bool _isRotationBlocked = true;

        private void Awake()
        {
            ServiceLocator.RegisterService(this);
        }

        private void Update()
        {
            if (_isRotationBlocked) { return; }
            RotateShip(_currentControlDriver.GetRotation(transform));
        }

        public void SetRotationBlock(bool isBlocked) => _isRotationBlocked = isBlocked;

        public void SetAutopilotDriver() => SetNewDriver(new ShipAutoPilotDriver());
        
        public void SetManualPilotDriver() => SetNewDriver(new ShipManualPilotDriver());

        private void SetNewDriver(IControlDriver newDriver) => _currentControlDriver = newDriver;
        
        private void RotateShip(Quaternion rotation) => transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10f * Time.deltaTime);
    }
}
