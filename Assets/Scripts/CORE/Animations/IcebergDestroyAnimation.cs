using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public interface AnimationBase
{
   public void Play();
   public void Play(Action callback);
}

public class IcebergDestroyAnimation : MonoBehaviour, AnimationBase
{
   [SerializeField]
   private float _duration = 0.5f;
   public void Play()
   {
      transform.DOMoveY(transform.position.y - 3f, _duration).SetEase(Ease.InBounce);
   }
   
   public void Play(Action callback)
   {
      transform.DOMoveY(transform.position.y - 3f, _duration).SetEase(Ease.InBounce).OnComplete(() =>
      {
         callback?.Invoke();
      });
   }
}
