using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class AnimationInvoker : MonoBehaviour
{
    [SerializeField]
    private AnimationBase animationReference;
    
    [SerializeField]
    private UnityEvent _onAnimationFinished;
    
    [Button]
    public void Play()
    {
        animationReference.Play(OnAnimationFinishedCallback);
    }

    private void OnAnimationFinishedCallback()
    {
        if(_onAnimationFinished == null) return;
        _onAnimationFinished.Invoke();
    }
}
