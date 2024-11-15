using Patterns.ServiceLocator;
using UnityEngine;

namespace CORE.Modules.Player.Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        private ShipStaticDataProvider _shipStaticDataProvider;
        private bool _isMovementBlocked;
        
        private void Awake()
        {
            ServiceLocator.RegisterService(this);
            _shipStaticDataProvider = ServiceLocator.GetService<ShipStaticDataProvider>();
        }
        
        private void Update()
        {
            if(_isMovementBlocked) {return;}
            MoveShip();
        }
        
        public void SetMovementBlock(bool isMovementBlocked) => _isMovementBlocked = isMovementBlocked;
        
        private void MoveShip() => transform.Translate(Vector3.forward * _shipStaticDataProvider.Data.ShipSpeed * Time.deltaTime);
    }
}
