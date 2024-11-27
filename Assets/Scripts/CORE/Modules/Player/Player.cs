using Patterns.ServiceLocator;
using UnityEngine;

namespace CORE.Modules.Player
{
    public class Player : MonoBehaviour
    {
        private void Awake()
        {
            ServiceLocator.RegisterService(this);
        }
    }
}
