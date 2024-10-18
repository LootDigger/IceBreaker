using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Patterns.AbstractStateMachine
{
    public interface IAbstractState
    {
        public virtual void EnterState(){}
        public virtual void ExitState(){}
        public virtual void UpdateState(){}
    }
}
