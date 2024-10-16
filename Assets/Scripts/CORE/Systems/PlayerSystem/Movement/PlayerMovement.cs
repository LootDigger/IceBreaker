using CORE.GameStates;
using Patterns.Observer;
using UnityEngine;

namespace CORE.Systems.PlayerSystem
{
    public class PlayerMovement : MonoBehaviour, IStateObserver
    {
        [SerializeField]
        private float _moveSpeed = 10f;

        private float _currentSpeed = 0f;

        public void OnSubjectStateEnter(IStateSubject stateSubject)
        {
            _currentSpeed = stateSubject is CORESM_InGame? _moveSpeed : 0f;
        }

        public void OnSubjectStateExit(IStateSubject stateSubject)
        {
        }
        
        void Update()
        {
            MoveShip();
        }
        
        private void MoveShip() => transform.Translate(Vector3.forward * _currentSpeed * Time.deltaTime);

    }
}
