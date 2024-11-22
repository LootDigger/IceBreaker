using Patterns.ServiceLocator;
using UnityEngine;

namespace Core.PlayerCamera
{
    public class CameraAnimator : MonoBehaviour,ICameraAnimator
    {
        [SerializeField]
        private CameraShakeAnimation _cameraShakeAnimation;

        private void Awake()
        {
            ServiceLocator.RegisterService(this);
        }

        public void PlayShakeAnimation()
        {
            _cameraShakeAnimation.Play();
        }
    }

    public interface ICameraAnimator
    {
        void PlayShakeAnimation();
    }
}