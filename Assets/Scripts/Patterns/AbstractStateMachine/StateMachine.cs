using UnityEngine;

namespace Patterns.AbstractStateMachine
{
    public class StateMachine
    {
        private AbstractState _currentState;
        
        public void SetState(AbstractState state)
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
            _currentState.UpdateState();
        }
    }
}
