using System;
using System.Collections.Generic;
using UnityEngine;

namespace Patterns.AbstractStateMachine
{
    public abstract class StateMachine
    {
        private Dictionary<Type, IState> _states = new();
        private IState _currentState;
        private IState _previousState;
        
        public IState CurrentState => _currentState;
        public IState PreviousState => _previousState;
        
        public void RegisterState(IState state)
        {
            _states.Add(state.GetType(), state);
        }

        public void SetState<TState>(bool debugSetState = false) where TState : IState
        {
            SetState(_states[typeof(TState)],debugSetState);
        }
        
        public void SetState(IState state, bool debugSetState = false)
        {
            if(debugSetState)
                Debug.Log("SetState: " + state.GetType().Name);
            
            if (_currentState != null)
            {
                _currentState.ExitState();
                _previousState = _currentState;
            }
            _currentState = state;
            _currentState.EnterState();
        }
        
        public void SubscribeStateEnter<TState>(Action eventHandler) where TState : IState
        {
            _currentState = _states[typeof(TState)];
            _currentState.OnEnterStateEvent += eventHandler;
        }
        
        public void UnsubscribeStateEnter<TState>(Action eventHandler) where TState : IState
        {
            _currentState = _states[typeof(TState)];
            _currentState.OnEnterStateEvent -= eventHandler;
        }
    }
}
