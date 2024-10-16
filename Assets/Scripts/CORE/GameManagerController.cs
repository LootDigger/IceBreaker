using Patterns.AbstractStateMachine;
using Patterns.ServiceLocator;
using UnityEngine;
using UnityEngine.Serialization;

namespace CORE
{
    [Tooltip("Controller in MVC architecture")]
    public class GameManagerController : MonoBehaviour
    {
        [FormerlySerializedAs("_manager")] [SerializeField]
        private GameManager _gameManager;

        void Start() => ServiceLocator.RegisterService(this);

        public void SetGameState(AbstractState state)
        {
            _gameManager.SetGameState(state);
        }
        
        public static GameManagerController GetInstance() => ServiceLocator.GetService<GameManagerController>();
    }
}
