using UnityEngine;

namespace Core.ShipControls.SM
{
    public class ManualPilotStateDeprecated : IStateDeprecated
    {
        public ShipStateMachine MachineReference { get; set; }
        
        private Transform _shipTransform;
        private float _edgeDetectionRayDistance;

        public IControlDriver Init(ShipStateMachine reference)
        {
            MachineReference = reference;
            _shipTransform = reference.MPSettings.playerTransform;
            _edgeDetectionRayDistance = reference.MPSettings.edgeDetectionRayDistance;
            return new ShipPlayerMovement();
        }

        public void Update() => WaitForLevelEdge();
    
        private void WaitForLevelEdge()
        {
            LayerMask mask  = LayerMask.GetMask("LevelEdge");
            Ray ray = new Ray(_shipTransform.position, _shipTransform.forward);
            Debug.DrawRay(_shipTransform.position, _shipTransform.forward);
            RaycastHit _currentHit;
            if (Physics.Raycast(ray, out _currentHit, _edgeDetectionRayDistance,mask))
            {
                MachineReference.SetState(new AutoPilotStateDeprecated());
            }
        }
    }
}
