using System;
using UnityEngine;
using DG.Tweening;

public class ShipDamagedAnimation :MonoBehaviour, AnimationBase
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _strength = new Vector3(10f, 0f, 10f);
    [SerializeField] private int _vibrato = 1;
    [SerializeField] private float _randomness = 50f;
    [SerializeField] private float _duration = 1f;

    public void Play()
    {
        _target.DOShakeRotation(_duration, _strength, _vibrato, _randomness);
    }
    
    public void Play(Action callback)
    {
        _target.DOShakeRotation(_duration, _strength, _vibrato, _randomness).OnComplete(() =>
        {
            callback?.Invoke();
        });
    }
}
