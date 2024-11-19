using UnityEngine;

namespace Core.ShipControls
{
    public class ShipManualPilotDriver : IControlDriver
    {
        public Quaternion GetRotation(Transform playerTransform)
        {
            return CalculateRotation(playerTransform,GetDestination());
        }
        
        public Vector3 GetDestination()
        {
            Vector3 targetPosition = Vector3.zero;
#if !UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_WEBGL
            
            Vector3 mouseScreenPosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition);
#elif UNITY_ANDROID || UNITY_IOS

            Vector2 touchPosition = GetTouchPosition();
            Ray ray = Camera.main.ScreenPointToRay(touchPosition);
#endif
            
            LayerMask mask = LayerMask.GetMask("Water");
            RaycastHit _currentHit;
            if (Physics.Raycast(ray, out _currentHit, Mathf.Infinity, mask))
            {
                targetPosition = _currentHit.point;
                targetPosition.y = 0.35f;
            }

            return targetPosition;
        }
        
#if (UNITY_ANDROID || UNITY_IOS) && UNITY_EDITOR 
        private Vector2 _cachedTouchPosition;

        private Vector2 GetTouchPosition()
        {
            if (Input.touchCount > 0)
            {
                _cachedTouchPosition = Input.GetTouch(0).position;
            }
            return _cachedTouchPosition;
        }
#endif

        private Quaternion CalculateRotation(Transform playerTransform, Vector3 targetPosition)
        {
            Debug.DrawLine(playerTransform.position, targetPosition, Color.red, Time.deltaTime);
            Vector3 direction = (targetPosition - playerTransform.position).normalized;
            return Quaternion.LookRotation(direction);
        }
    }

    public interface IControlDriver
    {
        Quaternion GetRotation(Transform playerTransform);
    }
}
