using System;
using Patterns.ServiceLocator;
using UnityEngine;

namespace CORE.Systems.PlayerSystem.Health
{
    public class HealthManager : MonoBehaviour
    {
        [SerializeField] 
        private int _maxHealthPoints = 3;
        
        public int CurrentHealthPoints { get; private set; }
        
        public event Action OnHealthDecreased;
        public event Action OnHealthReachedDeadPoint;

        private void Awake()
        {
            ServiceLocator.RegisterService(this);
            ResetHealthPoints();
        }
        
        public void DecreaseHealthPoint()
        {
            CurrentHealthPoints--;
            if (CurrentHealthPoints <= 0)
            {
                OnHealthReachedDeadPoint?.Invoke();
                return;
            }
            OnHealthDecreased?.Invoke();
        }

        public void ResetHealthPoints()
        {
            CurrentHealthPoints = _maxHealthPoints;
        }
    }
}
