using Patterns.ServiceLocator;
using UnityEngine;

namespace Core.PlayerCamera
{
    public class GameCamera : MonoBehaviour,IGameCamera
    {
        [SerializeField] private Transform _followTarget;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private float _smoothSpeed = 0.125f;
        [SerializeField] private float _cameraHeight;

        private bool _followPlayer = true;
        
        private void Awake()
        {
            ServiceLocator.RegisterService<IGameCamera>(this);
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

        public void PlayAnimation(AnimationBase animation)
        {
            animation.Play();
        }

        private void LookAtTarget() => transform.LookAt(_followTarget);

        private Vector3 ComposeDesiredPosition()
        {
            Vector3 position = _followTarget.position + _offset;
            position.y = _cameraHeight;
            return position;
        }

        private Vector3 ComposeDeltaPosition() =>
            Vector3.Lerp(transform.position, ComposeDesiredPosition(), _smoothSpeed);
    }

    public interface IGameCamera
    {
        void SetTargetFollowState(bool isFollowing);
        void ResetTransform();
    }
}
