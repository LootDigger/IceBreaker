using System;
using UnityEngine.UIElements;

public class VisualElementAnimation : AnimationBase
{
    protected VisualElement _animatedElement;
    
    public VisualElementAnimation(VisualElement element)
    {
        _animatedElement = element;
    }
    
    public virtual void Play()
    {
    }

    public virtual void Play(Action callback)
    {
    }
}
