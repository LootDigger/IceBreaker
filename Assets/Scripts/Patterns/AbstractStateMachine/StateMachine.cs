using System;
using System.Collections.Generic;
using UnityEngine;

namespace Patterns.AbstractStateMachine
{
    public abstract class StateMachine
    {
        protected Dictionary<Type, IState> _states;
        protected IState _currentState;
        protected IState _previousState;

        public StateMachine()
        {
            _states = new();
        }

        public void RegisterState(IState state)
        {
            _states.Add(state.GetType(), state);
        }

        public void SetState<TState>() where TState : IState
        {
            if (_currentState != null)
            {
                _previousState = _currentState;
                _currentState.ExitState();
            }
            _currentState = _states[typeof(TState)];
            _currentState.EnterState();
        }

        public void UpdateMachine()
        {
            if (_currentState == null)
            {
                Debug.LogWarning("Looks like current state of SM is NULL");
                return;
            }
            _currentState.UpdateState();
        }
    }
}
