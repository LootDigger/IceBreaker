using Core.ShipControls.SM;
using UnityEngine;

namespace Core.ShipControls
{
    public class ShipMovementManager : MonoBehaviour
    {
        [SerializeField]
        private float _moveSpeed = 10f;  
        [SerializeField]
        private Camera _mainCamera;
        [SerializeField] 
        private float _autoPilotCooldown = 5f;

        private IMoveModule _currentMoveModule = null;
        private ShipStateMachine _currentShipStateMachine;

        private void Start() => InitMachine();

        void Update()
        {
            _currentShipStateMachine.UpdateMachine();
            RotateShip(CalculateRotation(_currentMoveModule.CalculateTargetPosition()));
            MoveShip();
        }

        private void OnDestroy() => UnsubscribeEvents();
    
        private void InitMachine()
        {
            _currentShipStateMachine = new ShipStateMachine(
                new ShipStateMachine.AutoPilotSettings()
                {
                    autoPilotDuration = _autoPilotCooldown
                },
                new ShipStateMachine.ManualPilotSettings()
                {
                    playerTransform = transform,
                    edgeDetectionRayDistance = 20f
                }
            );
            SubscribeEvents();
            _currentShipStateMachine.InitMachine();
        }

        private void SetNewModule(IMoveModule newModule) => _currentMoveModule = newModule;

        private void SubscribeEvents() => _currentShipStateMachine.OnStateChanged += SetNewModule;
    
        private void UnsubscribeEvents() => _currentShipStateMachine.OnStateChanged -= SetNewModule;
    
        private Quaternion CalculateRotation(Vector3 targetPosition)
        {
            Debug.DrawLine(transform.position,targetPosition,Color.red,Time.deltaTime);
            Vector3 direction = (targetPosition - transform.position).normalized;
            return Quaternion.LookRotation(direction);
        }
    
        private void RotateShip(Quaternion rotation) => transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10f * Time.deltaTime);
        
        private void MoveShip() => transform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime);
    }
}