using System;
using System.Collections.Generic;
using UnityEngine;

namespace Patterns.AbstractStateMachine
{
    public abstract class StateMachine
    {
        private Dictionary<Type, IState> _states;
        private IState _currentState;

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
                _currentState.ExitState();
            }
            _currentState = _states[typeof(TState)];
            _currentState.EnterState();
        }
    }
}
