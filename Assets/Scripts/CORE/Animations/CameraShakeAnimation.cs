using System;
using CORE.Systems.PlayerSystem.Health;
using UnityEngine;
using DG.Tweening;
using Patterns.ServiceLocator;

namespace Core.PlayerCamera
{
    public class CameraShakeAnimation : MonoBehaviour, AnimationBase
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _duration = 0.25f;

        /// <summary>
        /// Curve represents the Health/Shake Strength ratio
        /// </summary>
        [SerializeField] private AnimationCurve _strengthCurve;

        /// <summary>
        /// Curve represents the Health/Vibro Strength ratio
        /// </summary>
        [SerializeField] private AnimationCurve _vibrationCurve;

        private float Strength =>
            _strengthCurve.Evaluate(_playerHealth.CurrentHealthPoints);

        private int Vibrato =>
            (int)_vibrationCurve.Evaluate(_playerHealth.CurrentHealthPoints);

        private HealthManager _playerHealth;

        private void Start()
        {
            _playerHealth = ServiceLocator.GetService<HealthManager>();
        }

        public void Play()
        {
            _target.DOShakePosition(_duration, Strength, Vibrato);
        }
        
        public void Play(Action callback)
        {
            _target.DOShakePosition(_duration, Strength, Vibrato).OnComplete(() =>
            {
                callback?.Invoke();
            });;;
        }
    }
}