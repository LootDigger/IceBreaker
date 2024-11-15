using System;
using CORE.GameStates;
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
            if (_isRotationBlocked) {return;}
            RotateShip(CalculateRotation(_currentControlDriver.GetDestination()));
        }

        public void SetRotationBlock(bool isBlocked) => _isRotationBlocked = isBlocked;

        public void SetAutopilotDriver() => SetNewDriver(new ShipAutoPilotDriver());
        
        public void SetManualPilotDriver() => SetNewDriver(new ShipManualPilotDriver());

        private void SetNewDriver(IControlDriver newDriver) => _currentControlDriver = newDriver;
        
        private Quaternion CalculateRotation(Vector3 targetPosition)
        {
            Debug.DrawLine(transform.position,targetPosition,Color.red,Time.deltaTime);
            Vector3 direction = (targetPosition - transform.position).normalized;
            return Quaternion.LookRotation(direction);
        }
        
        private void RotateShip(Quaternion rotation) => transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10f * Time.deltaTime);
    }
}
