using System;
using Patterns.AbstractStateMachine;
using CORE.Modules.PlayerSystem.SM;
using Patterns.Command;
using Patterns.ServiceLocator;
using UnityEngine;

namespace CORE.GameStates
{
    public class CORE_BootstrapState : IState
    {
        public StateMachine StateMachine { get; set; }

        public CORE_BootstrapState(StateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        public void EnterState()
        {
            Debug.Log("CORE: Enter State Bootstrap");
            RegisterServices();
            StateMachine.SetState<CORE_InitState>();
        }

        private void RegisterServices()
        {
            ServiceLocator.RegisterService(new CommandExecuter());
            ServiceLocator.RegisterService(new ShipStateMachine());
        }
    }
}
