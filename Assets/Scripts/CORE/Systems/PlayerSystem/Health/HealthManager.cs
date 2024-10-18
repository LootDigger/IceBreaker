using System;
using CORE.Systems.PlayerSystem.SM;
using CORE.Systems.PlayerSystem.SM.Commands;
using Patterns.Command;
using Patterns.ServiceLocator;
using UnityEngine;

namespace CORE.Systems.PlayerSystem.Health
{
    public class HealthManager : MonoBehaviour
    {
        [SerializeField] private int _maxHealthPoints = 3;
        private int _currentHealthPoints;

        private void Awake()
        {
            ResetHealthPoints();
        }
        
        private void Start()
        {
            ServiceLocator.RegisterService(this);
        }
        
        public void DecreaseHealthPoint()
        {
            _currentHealthPoints--;
            if (_currentHealthPoints <= 0)
            {
                CommandExecuter.ExecuteCommand(new SetDeadStateCommand());
            }
        }

        public void ResetHealthPoints()
        {
            _currentHealthPoints = _maxHealthPoints;
        }
    }
}
