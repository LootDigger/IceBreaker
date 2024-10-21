using System;
using CORE.Modules.PlayerSystem.SM;
using CORE.Systems.PlayerSystem.SM;
using Patterns.AbstractStateMachine;
using Patterns.Command;
using Patterns.ServiceLocator;
using UnityEngine;

namespace CORE.Systems.PlayerSystem.Health
{
    public class HealthManager : MonoBehaviour
    {
        [SerializeField] 
        private int _maxHealthPoints = 3;
        
        private int _currentHealthPoints;
        private StateMachine _shipStateMachine;

        private void Awake()
        {
            ServiceLocator.RegisterService(this);
            ResetHealthPoints();
        }
        
        private void Start()
        {
            _shipStateMachine = ServiceLocator.GetService<ShipStateMachine>();
        }
        
        public void DecreaseHealthPoint()
        {
            _currentHealthPoints--;
            if (_currentHealthPoints <= 0)
            {
                _shipStateMachine.SetState<SHIP_DeadState>();
            }
        }

        public void ResetHealthPoints()
        {
            _currentHealthPoints = _maxHealthPoints;
        }
    }
}
