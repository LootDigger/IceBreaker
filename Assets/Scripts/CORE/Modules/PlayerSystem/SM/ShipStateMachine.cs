using CORE.Systems.PlayerSystem;
using CORE.Systems.PlayerSystem.Health;
using CORE.Systems.PlayerSystem.Movement;
using CORE.Systems.PlayerSystem.SM;
using Patterns.AbstractStateMachine;
using Patterns.ServiceLocator;

namespace CORE.Modules.PlayerSystem.SM
{
    public class ShipStateMachine : StateMachine
    {
        private PlayerMovement _playerMovement;
        private PlayerRotation _playerRotation;
        private HealthManager _playerHealth;
        
        public ShipStateMachine()
        {
            GetServices();
            InitStateMachine();
            SetInitState();
        }

        private void SetInitState() => SetState<SHIP_IdleState>();

        private void GetServices()
        {
            _playerRotation = ServiceLocator.GetService<PlayerRotation>();
            _playerMovement = ServiceLocator.GetService<PlayerMovement>();
            _playerHealth = ServiceLocator.GetService<HealthManager>();
        }
        
        private void InitStateMachine()
        {
            RegisterState(new SHIP_IdleState(this,_playerRotation, _playerMovement));
            RegisterState(new SHIP_GameInitState(this));
            RegisterState(new SHIP_ManualPilotState(this,_playerRotation, _playerMovement));
            RegisterState(new SHIP_AutopilotState(this,_playerRotation));
            RegisterState(new SHIP_ShootState(this));
            RegisterState(new SHIP_TakeDamageState(this,_playerHealth));
            RegisterState(new SHIP_DeadState(this,_playerRotation,_playerMovement));
        }
    }
}
