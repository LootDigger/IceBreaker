using CORE.GameStates;
using CORE.Modules.PlayerSystem.SM;
using Patterns.Command;
using Patterns.ServiceLocator;
using Scene_Management;
using UnityEngine;

namespace CORE.Bootstrap
{
    public class Bootstrap : MonoBehaviour
    {
        private void Start() => Init();

        private void Init()
        {
            RegisterServices();
        }

        private void RegisterServices()
        {
            ServiceLocator.RegisterService(new CommandExecuter());
            ServiceLocator.RegisterService(new SceneLoader());
            
            CoreStateMachine stateMachine = new CoreStateMachine();
            ServiceLocator.RegisterService(stateMachine);
            stateMachine.SetState<CORE_InitState>();
        }
    }
}
