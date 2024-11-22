using System;
using System.Threading.Tasks;
using CORE.Modules.Player.Movement;
using Core.PlayerCamera;
using CORE.Systems.PlayerSystem.Health;
using CORE.Systems.PlayerSystem.Movement;
using Patterns.AbstractStateMachine;
using Patterns.ServiceLocator;

namespace CORE.Modules.Player.SM
{
    public class SHIP_TakeDamageState : IState, IDisposable
    {
        private readonly HealthManager _healthManager;
        private readonly PlayerMovement _movement;
        private readonly PlayerRotation _rotation;
        private readonly AnimationInvoker _shipDamageAnimation;
        private readonly ShipStaticDataProvider _shipStaticDataProvider;
        private readonly ICameraAnimator _cameraAnimator;


        public StateMachine StateMachine { get; set; }
        public Action OnEnterStateEvent { get; set; }
        public Action OnExitStateEvent { get; set; }
        
        public SHIP_TakeDamageState(StateMachine stateMachine, HealthManager healthManager,PlayerMovement movement,PlayerRotation rotation, AnimationInvoker animation)
        {
            StateMachine = stateMachine;
            _healthManager = healthManager;
            _movement = movement;
            _rotation = rotation;
            _shipDamageAnimation = animation;
            _shipStaticDataProvider = ServiceLocator.GetService<ShipStaticDataProvider>();
            _cameraAnimator = ServiceLocator.GetService<CameraAnimator>();
            SubscribeEvents();
        }
        
        public void EnterState()
        {
            OnEnterStateEvent?.Invoke();
            _movement.SetMovementBlock(true);
            _rotation.SetRotationBlock(true);
            _healthManager.DecreaseHealthPoint();
            _cameraAnimator.PlayShakeAnimation();
        }

        public void ExitState()
        {
            OnExitStateEvent?.Invoke();
        }

        private void SubscribeEvents()
        {
            _healthManager.OnHealthReachedDeadPoint += OnHealthReachedDeadPointHandler;
            _healthManager.OnHealthDecreased += OnHealthDecreasedHandler;
        }

        private void UnsubscribeEvents()
        {
            _healthManager.OnHealthReachedDeadPoint -= OnHealthReachedDeadPointHandler;
            _healthManager.OnHealthDecreased -= OnHealthDecreasedHandler;
        }
        
        private async Task DamageTakeRoutine()
        {
            _shipDamageAnimation.Play();
            await Task.Delay((int)(_shipStaticDataProvider.Data.DamageFreezeTime * 1000));
            StateMachine.SetState(StateMachine.PreviousState);
        }

        private void OnHealthReachedDeadPointHandler()
        {
            StateMachine.SetState<SHIP_DeadState>();
        }
        
        private void OnHealthDecreasedHandler()
        {
            DamageTakeRoutine();
        }

        public void Dispose() => UnsubscribeEvents();
    }
}
