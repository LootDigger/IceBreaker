using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class AnimationBase : MonoBehaviour
{
   public abstract void Play();
   public abstract void Play(Action callback);
}

public class IcebergDestroyAnimation : AnimationBase
{
   [SerializeField]
   private float _duration = 0.5f;
   public override void Play()
   {
      transform.DOMoveY(transform.position.y - 3f, _duration).SetEase(Ease.InBounce);
   }
   
   public override void Play(Action callback)
   {
      transform.DOMoveY(transform.position.y - 3f, _duration).SetEase(Ease.InBounce).OnComplete(() =>
      {
         callback?.Invoke();
      });
   }
}
