using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class GameLogoAnimation : VisualElementAnimation
{
    private float _duration;
    
    public GameLogoAnimation(VisualElement element, float duration) : base(element)
    {
        _duration = duration;
    }

    public override void Play()
    {
        AnimateRoutine(_duration);
    }

    public override void Play(Action callback)
    {
        AnimateRoutine(_duration,callback);
    }

    private async UniTaskVoid AnimateRoutine(float duration, Action callback = null)
    {
        await UniTask.Delay(2000);
        Color currentColor = Color.white;
        Debug.Log("Initial Color " + currentColor);
        await DOTween.To(
            () => currentColor.a,
            alpha =>
            {
                currentColor.a = alpha;
                _animatedElement.style.color = currentColor;
                Debug.Log("Delta Color " + currentColor);

            },
            0f,
            duration
        ).SetEase(Ease.InCirc).OnComplete(()=>{callback?.Invoke();});
    }
}
