using Patterns.ServiceLocator;
using UnityEngine;

namespace Core.PlayerCamera
{
    public class CameraFollower : MonoBehaviour,ICameraFollower
    {
        [SerializeField] private Transform _followTarget;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private float _smoothSpeed = 0.125f;

        private bool _followPlayer = true;
        
        private void Awake()
        {
            ServiceLocator.RegisterService<ICameraFollower>(this);
        }

        void LateUpdate()
        {
            if (_followTarget == null || !_followPlayer) return;
            transform.position = ComposeDeltaPosition();
            LookAtTarget();
        }
        
        public void SetTargetFollowState(bool isFollowing)
        {
            _followPlayer = isFollowing;
        }

        [Sirenix.OdinInspector.Button]
        public void ResetTransform()
        {
            transform.position = ComposeDesiredPosition();
            LookAtTarget();
        }

        private void LookAtTarget() => transform.LookAt(_followTarget);
        private Vector3 ComposeDesiredPosition() => _followTarget.position + _offset;
        private Vector3 ComposeDeltaPosition() =>
            Vector3.Lerp(transform.position, ComposeDesiredPosition(), _smoothSpeed);
    }

    public interface ICameraFollower
    {
        void SetTargetFollowState(bool isFollowing);
        void ResetTransform();
    }
}
