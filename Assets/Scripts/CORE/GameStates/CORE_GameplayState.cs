using System;
using CORE.Modules.Player.SM;
using Core.PlayerCamera;
using Patterns.AbstractStateMachine;
using Patterns.ServiceLocator;

namespace CORE.GameStates
{
    public class CORE_GameplayState : IState
    {
        private ShipStateMachine ShipStateMachine => ServiceLocator.GetService<ShipStateMachine>();
        private ICameraFollower CameraFollower => ServiceLocator.GetService<ICameraFollower>();
        public StateMachine StateMachine { get; set; }
        public Action OnEnterStateEvent { get; set; }
        public Action OnExitStateEvent { get; set; }
        
        public CORE_GameplayState(StateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        public void EnterState()
        {
            OnEnterStateEvent?.Invoke();
            ShipStateMachine.SetState<SHIP_GameInitState>();
            UIEventDocker.OnGameplayUIShown.Invoke();
            CameraFollower.ResetTransform();
            CameraFollower.SetTargetFollowState(true);
        }

        public void ExitState()
        {
            OnExitStateEvent?.Invoke();
        }
    }
}
