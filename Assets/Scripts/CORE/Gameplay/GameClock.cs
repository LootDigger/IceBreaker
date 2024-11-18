using System;
using UnityEngine;

namespace CORE.GameManager
{
    public class GameClock  : MonoBehaviour
    {
        private bool _isRunning = false;
        private float _elapsedTime = 0f;
        private int _secondsElapsed = 0;

        public int SecondsElapsed => _secondsElapsed;
        
        public event Action<int> OnSecondPassed;
        
        public void StartClock()
        {
            _isRunning = true;
            _elapsedTime = 0f;
            _secondsElapsed = 0;
        }
        
        public void StopClock() => _isRunning = false;

        private void Update()
        {
            if (!_isRunning) return;

            _elapsedTime += Time.deltaTime;

            if (_elapsedTime >= 1f)
            {
                _elapsedTime -= 1f;
                _secondsElapsed++;
                OnSecondPassed?.Invoke(_secondsElapsed);
            }
        }
    }
}
