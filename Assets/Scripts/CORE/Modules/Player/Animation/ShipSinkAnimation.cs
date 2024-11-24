using System;
using UnityEngine;
using DG.Tweening;

public class ShipSinkAnimation : AnimationBase
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _duration = 2f;
    
    private Vector3 _sinkedRotation = new Vector3(-180f, 0f, -90f);

    public override void Play()
    {
        _target.DOMoveY(transform.position.y - 4f, _duration).SetEase(Ease.InOutElastic);
        _target.DOLocalRotate(_sinkedRotation, _duration);
    }
    
    public override void Play(Action callback)
    {
        _target.DOMoveY(transform.position.y - 4f, _duration).SetEase(Ease.InOutElastic);
        _target.DOLocalRotate(_sinkedRotation, _duration).OnComplete(() =>
        {
            callback?.Invoke();
        });;
    }
}
