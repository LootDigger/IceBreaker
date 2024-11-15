using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Patterns.AbstractStateMachine
{
    public interface IState
    {
        public StateMachine StateMachine { get; set; }
        public Action OnEnterStateEvent{ get; set; }
        public Action OnExitStateEvent{ get; set; }

        public void EnterState();

        public void ExitState();
    }
}
