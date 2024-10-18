using UnityEngine;

namespace Core.ShipControls
{
    public class ShipManualPilotDriver : IControlDriver
    {
        public Vector3 GetDestination()
        {
            Vector3 targetPosition = Vector3.zero;
            Vector3 mouseScreenPosition = Input.mousePosition;
        
            Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition);
            LayerMask mask  = LayerMask.GetMask("Water");

            RaycastHit _currentHit;
            if (Physics.Raycast(ray, out _currentHit, Mathf.Infinity,mask))
            {
                targetPosition = _currentHit.point;
                targetPosition.y = 0.35f;
            }
            return targetPosition;
        }
    }

    public interface IControlDriver
    {
        Vector3 GetDestination(); 
    }
}