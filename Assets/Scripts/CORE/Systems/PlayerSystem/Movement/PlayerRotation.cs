using CORE.GameStates;
using Core.ShipControls;
using CORE.Systems.PlayerSystem.SM;
using Patterns.Observer;
using UnityEngine;

namespace CORE.Systems.PlayerSystem.Movement
{
    public class PlayerRotation : MonoBehaviour, IStateObserver
    {
        private IControlDriver _currentControlDriver = null;
        private bool _isRotationBlocked = true;
        
        void Update()
        {
            if (_isRotationBlocked) {return;}
            RotateShip(CalculateRotation(_currentControlDriver.CalculateTargetPosition()));
        }
        
        public void OnSubjectStateEnter(IStateSubject stateSubject)
        {
            Debug.Log("OnSubjectStateEnter Rotation");
            if (stateSubject is CORESM_GameOver)
            {
                _isRotationBlocked = true;
            }

            if (stateSubject is SHIPSM_Autopilot)
            {
                SetNewDriver(new ShipAutoPilotMovement());
            }
            
            if (stateSubject is SHIPSM_ManualControl)
            {
                _isRotationBlocked = false;
                SetNewDriver(new ShipPlayerMovement());
            }
        }

        public void OnSubjectStateExit(IStateSubject stateSubject)
        {
        }

        private void SetNewDriver(IControlDriver newDriver) => _currentControlDriver = newDriver;
        
        private Quaternion CalculateRotation(Vector3 targetPosition)
        {
            Debug.Log("Target position: " + targetPosition);
            Debug.DrawLine(transform.position,targetPosition,Color.red,Time.deltaTime);
            Vector3 direction = (targetPosition - transform.position).normalized;
            return Quaternion.LookRotation(direction);
        }
        
        private void RotateShip(Quaternion rotation) => transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10f * Time.deltaTime);
    }
}
