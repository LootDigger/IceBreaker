using UnityEngine;

namespace CORE.GameManager
{
    public class GameWaiter 
    {
        private float _startTime;

        public void StartWaiter()
        {
            _startTime = Time.time;
            Debug.Log("Таймер запущен!");
        }

        public float StopWaiter()
        {
            float elapsedTime = Time.time - _startTime;
            Debug.Log("Таймер остановлен! Время: " + elapsedTime + " секунд");
            return elapsedTime;
        }
    }
}
