using CORE.GameStates;
using Patterns.ServiceLocator;
using UnityEngine;

namespace CORE.Bootstrap
{
    public class Bootstrap : MonoBehaviour
    {
        void Start() => InitSM();

        private void InitSM()
        {
            CoreStateMachine stateMachine = new CoreStateMachine();
            ServiceLocator.RegisterService(stateMachine);
            stateMachine.SetState<CORE_BootstrapState>();
        }
    }
}
