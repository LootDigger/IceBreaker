using System.Collections.Generic;
using Patterns.Observer;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Patterns.AbstractStateMachine
{
    public abstract class AbstractState : SerializedMonoBehaviour, IStateSubject
    {
        [SerializeField][OdinSerialize]
        private List<IStateObserver> _stateObservers = new();
        

        public virtual void EnterState() => NotifyOnEnter();
        public virtual void ExitState() => NotifyOnExit();
        public virtual void UpdateState(){}
        
        public void NotifyOnEnter()
        {
            foreach (IStateObserver observer in _stateObservers)
            {
                observer.OnSubjectStateEnter(this);
            }
        }

        public void NotifyOnExit()
        {
            foreach (IStateObserver observer in _stateObservers)
            {
                observer.OnSubjectStateExit(this);
            }
        }
    }
}
