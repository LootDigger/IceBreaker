using System;
using Patterns.AbstractStateMachine;
using System.Threading.Tasks;
using CORE.Modules.PlayerSystem.SM;
using Patterns.ServiceLocator;
using Scene_Management;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CORE.GameStates
{
    public class CORE_InitState : IState
    {
        private SceneLoader _sceneLoader;
        public StateMachine StateMachine { get; set; }
        
        public CORE_InitState(StateMachine stateMachine, SceneLoader sceneLoader)
        {
            StateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void EnterState()
        {
            Debug.Log("CORE: Enter State Init " + Time.time);
            InitState();
        }

        private async Task InitState()
        {
            await InitWaiter();
            await LoadGameScene();
            Debug.Log("Load Game Menu State");
            StateMachine.SetState<CORE_GameMenuState>();
        }

        private async Task LoadGameScene()
        {
            await _sceneLoader.LoadScene(new LoadSceneRequest()
            {
                SceneName = "Scenes/GameScene",
                LoadSceneMode = LoadSceneMode.Single
            });
        }

        private async Task InitWaiter()
        {
            GameInitWaiter dummyWaiter = new GameInitWaiter(5000);
            await dummyWaiter.WaitForGameLoad();
        }

        private class GameInitWaiter
        {
            private int waitTime;
            public GameInitWaiter(int time) => waitTime = time;
            public async Task WaitForGameLoad() => await Task.Delay(waitTime);
        }
    }
}
