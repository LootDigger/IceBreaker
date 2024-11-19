using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class AnimationInvoker : MonoBehaviour
{
    [SerializeField]
    private AnimationBase animationReference;
    
    [Button]
    public void Play()
    {
        animationReference.Play();
    }
}
