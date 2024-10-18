using UnityEngine;

namespace Patterns.AbstractStateMachine
{
    public class StateMachine
    {
        private IAbstractState _currentState;
        
        public void SetState(IAbstractState state)
        {
            if (_currentState != null)
            {
                _currentState.ExitState();
            }
            _currentState = state;
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
