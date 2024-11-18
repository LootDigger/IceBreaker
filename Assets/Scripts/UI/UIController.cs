using System;
using CORE.GameStates;
using Patterns.AbstractStateMachine;
using Patterns.ServiceLocator;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField]
        private VisualElementPresenter _playMenuUI;
        [SerializeField]
        private VisualElementPresenter _inGameUI;
        
        private VisualElement _rootElement;
        private StateMachine _coreStateMachine;

        private void Awake()
        {
            Init();
            SetupPresenters();
            SubscribeGameEvents();
        }

        private void Start()
        {
            
        }

        private void OnDestroy() => UnsubscribeGameEvents();
        
        private void Init()
        {
            _rootElement = FindObjectOfType<UIDocument>().rootVisualElement;
            _coreStateMachine = ServiceLocator.GetService<CoreStateMachine>();
        }

        private void SetupPresenters()
        {
            _playMenuUI.SetRootElement(_rootElement);
            _inGameUI.SetRootElement(_rootElement);
        }

        private void SubscribeGameEvents()
        {
            _coreStateMachine.SubscribeStateEnter<CORE_GameMenuState>(_playMenuUI.ShowElement);
            _coreStateMachine.SubscribeStateEnter<CORE_GameplayState>(_playMenuUI.HideElement);
            _coreStateMachine.SubscribeStateEnter<CORE_GameplayState>(_inGameUI.ShowElement);
        }
        
        private void UnsubscribeGameEvents()
        {
            _coreStateMachine.UnsubscribeStateEnter<CORE_GameMenuState>(_playMenuUI.ShowElement);
            _coreStateMachine.UnsubscribeStateEnter<CORE_GameplayState>(_playMenuUI.HideElement);
            _coreStateMachine.UnsubscribeStateEnter<CORE_GameplayState>(_inGameUI.ShowElement);
        }
    }
}
