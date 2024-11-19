using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class AnimationBase : MonoBehaviour
{
   public abstract void Play();
}

public class IcebergDestroyAnimation : AnimationBase
{
   public override void Play()
   {
      transform.DOMoveY(transform.position.y - 3f, 1f).SetEase(Ease.InBounce);
   }
}
