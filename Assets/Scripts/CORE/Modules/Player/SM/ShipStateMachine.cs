using CORE.Modules.Player.Movement;
using CORE.Systems.PlayerSystem;
using CORE.Systems.PlayerSystem.Health;
using CORE.Systems.PlayerSystem.Movement;
using Patterns.AbstractStateMachine;
using Patterns.ServiceLocator;

namespace CORE.Modules.Player.SM
{
    public class ShipStateMachine : StateMachine
    {
        private PlayerMovement _playerMovement;
        private PlayerRotation _playerRotation;
        private HealthManager _playerHealth;
        private MapEdgeDetector _mapEdgeDetector;
        
        public ShipStateMachine()
        {
            GetServices();
            InitStateMachine();
            SetInitState();
        }
        
        private void GetServices()
        {
            _playerMovement = ServiceLocator.GetService<PlayerMovement>();
            _playerRotation = ServiceLocator.GetService<PlayerRotation>();
            _playerHealth = ServiceLocator.GetService<HealthManager>();
            _mapEdgeDetector = ServiceLocator.GetService<MapEdgeDetector>();
        }
        
        private void InitStateMachine()
        {
            RegisterState(new SHIP_IdleState(this,_playerRotation, _playerMovement));
            RegisterState(new SHIP_GameInitState(this,_playerHealth));
            RegisterState(new SHIP_ManualPilotState(this,_playerRotation, _playerMovement,_mapEdgeDetector));
            RegisterState(new SHIP_AutopilotState(this,_playerRotation));
            RegisterState(new SHIP_TakeDamageState(this,_playerHealth));
            RegisterState(new SHIP_DeadState(this,_playerRotation,_playerMovement));
        }
        
        private void SetInitState() => SetState<SHIP_IdleState>();
    }
}
