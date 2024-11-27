using System.Collections.Generic;
using CORE.GameStates;
using Cysharp.Threading.Tasks;
using Patterns.AbstractStateMachine;
using Patterns.ServiceLocator;
using Sirenix.OdinInspector;
using UnityEngine;

public class GamePreloader : SerializedMonoBehaviour
{
    [SerializeField]
    private List<ILoader> _loaders = new();

    private StateMachine StateMachine;
    
    async void Start()
    { 
        Init();
        await PreloadAsync();
    }

    private void Init()
    {
        StateMachine = ServiceLocator.GetService<CoreStateMachine>();
    }

    public async UniTask PreloadAsync()
    {
        foreach (var loader in _loaders)
        {
            await loader.Init();
        }
        StateMachine.SetState<CORE_GameMenuState>();
    }
}