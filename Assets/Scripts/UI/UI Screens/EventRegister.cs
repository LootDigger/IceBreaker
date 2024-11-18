using System;
using UnityEngine.UIElements;

public class EventRegister
{
    private Action _unregisterCallbacks;
    
    public void RegisterCallback<TEvent>(VisualElement visualElement, Action callback) where TEvent : EventBase<TEvent>,new()
    {
        EventCallback<TEvent> callbackDelegate = new((evt) => callback.Invoke());
        visualElement.RegisterCallback(callbackDelegate);
        _unregisterCallbacks += ()=>{visualElement.UnregisterCallback(callbackDelegate);};
    }
    
    public void RegisterCallback<TEvent>(VisualElement visualElement, Action<TEvent> callback) where TEvent : EventBase<TEvent>,new()
    {
        EventCallback<TEvent> callbackDelegate = new(callback);
        visualElement.RegisterCallback(callbackDelegate);
        _unregisterCallbacks += ()=>{visualElement.UnregisterCallback(callbackDelegate);};
    }
    
    public void RegisterValueChangeCallback<T>(BindableElement bindableElement, Action<T> callback) where T : struct
    {
        EventCallback<ChangeEvent<T>> callbackDelegate = new((evt) => callback.Invoke(evt.newValue));
        bindableElement.RegisterCallback(callbackDelegate);
        _unregisterCallbacks += ()=>{bindableElement.UnregisterCallback(callbackDelegate);};
    }
    
    public void Dispose()
    {
        _unregisterCallbacks?.Invoke();
        _unregisterCallbacks = null;
    }
}
