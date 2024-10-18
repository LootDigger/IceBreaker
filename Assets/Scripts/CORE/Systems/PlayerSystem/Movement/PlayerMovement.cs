using Patterns.ServiceLocator;
using UnityEngine;

namespace CORE.Systems.PlayerSystem
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private float _moveSpeed = 10f;

        private bool _isMovementBlocked;
        
        private void Awake()
        {
            ServiceLocator.RegisterService(this);
        }
        
        private void Update()
        {
            if(_isMovementBlocked) {return;}
            MoveShip();
        }
        
        public void SetMovementBlock(bool isMovementBlocked) => _isMovementBlocked = isMovementBlocked;
        
        private void MoveShip() => transform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime);
    }
}
