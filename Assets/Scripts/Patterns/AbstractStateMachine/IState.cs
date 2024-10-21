using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Patterns.AbstractStateMachine
{
    public interface IState
    {
        public StateMachine StateMachine { get; set; }
        public virtual void EnterState(){}
        public virtual void ExitState(){}
    }
}
