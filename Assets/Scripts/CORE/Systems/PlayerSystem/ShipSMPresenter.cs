using System;
using CORE.GameStates;
using Core.ShipControls.SM;
using CORE.Systems.PlayerSystem.SM;
using Patterns.AbstractStateMachine;
using Patterns.Observer;
using Patterns.ServiceLocator;
using UnityEngine;

namespace Core.ShipControls
{
    public class ShipSMPresenter : MonoBehaviour, IStateObserver
    {
        private StateMachine _shipStateMachine;

        private void Awake()
        {
            ServiceLocator.RegisterService(this);
        }

        void Start()
        {
            InitSM();
        }

        private void Update()
        {
            _shipStateMachine.UpdateMachine();
        }

        private void InitSM()
        {
            _shipStateMachine = new StateMachine();
            SetState(ServiceLocator.GetService<SHIPSM_Idle>());
        }

        public void SetState(AbstractState state)
        {
            _shipStateMachine.SetState(state);
        }
        
        public void OnSubjectStateEnter(IStateSubject stateSubject)
        {
            if (stateSubject is CORESM_InGame)
            {
                ServiceLocator.GetService<ShipSMPresenter>().SetState(ServiceLocator.GetService<SHIPSM_ManualControl>());
            }
        }

        public void OnSubjectStateExit(IStateSubject stateSubject)
        {
        }
    }
}