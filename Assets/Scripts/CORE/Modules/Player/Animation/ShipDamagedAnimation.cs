using UnityEngine;
using DG.Tweening;

public class ShipDamagedAnimation : AnimationBase
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _strength = new Vector3(10f, 0f, 10f);
    [SerializeField] private int _vibrato = 1;
    [SerializeField] private float _randomness = 50f;
    [SerializeField] private float _duration = 1.5f;

    public override void Play()
    {
        _target.DOShakeRotation(_duration, _strength, _vibrato, _randomness);
    }
}
