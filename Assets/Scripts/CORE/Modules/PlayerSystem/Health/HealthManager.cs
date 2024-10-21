using System;
using Patterns.ServiceLocator;
using UnityEngine;

namespace CORE.Systems.PlayerSystem.Health
{
    public class HealthManager : MonoBehaviour
    {
        [SerializeField] 
        private int _maxHealthPoints = 3;
        
        private int _currentHealthPoints;
        public Action OnHealthReachedDeadPoint;

        private void Awake()
        {
            ServiceLocator.RegisterService(this);
            ResetHealthPoints();
        }
        
        public void DecreaseHealthPoint()
        {
            _currentHealthPoints--;
            if (_currentHealthPoints <= 0)
            {
                OnHealthReachedDeadPoint?.Invoke();
            }
        }

        public void ResetHealthPoints()
        {
            _currentHealthPoints = _maxHealthPoints;
        }
    }
}
