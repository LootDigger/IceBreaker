using UnityEngine;

namespace Core.ShipControls
{
    public class ShipAutoPilotDriver : IControlDriver
    {
        public Vector3 GetDestination() => Vector3.zero;
        
        public Quaternion GetRotation(Transform playerTransform)
        {
            return CalculateRotation(playerTransform, GetDestination());
        }
        
        private Quaternion CalculateRotation(Transform playerTransform, Vector3 targetPosition)
        {
            Debug.DrawLine(playerTransform.position, targetPosition, Color.red, Time.deltaTime);
            Vector3 direction = (targetPosition - playerTransform.position).normalized;
            return Quaternion.LookRotation(direction);
        }
    }
}
