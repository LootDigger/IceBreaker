using System;
using Patterns.AbstractStateMachine;
using System.Threading.Tasks;
using Scene_Management;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CORE.GameStates
{
    public class CORE_InitState : IState
    {
        private SceneLoader _sceneLoader;
        public StateMachine StateMachine { get; set; }
        public Action OnEnterStateEvent { get; set; }
        public Action OnExitStateEvent { get; set; }

        public CORE_InitState(StateMachine stateMachine, SceneLoader sceneLoader)
        {
            StateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void EnterState()
        {
            OnEnterStateEvent?.Invoke();
            InitState();
        }

        public void ExitState()
        {
            OnExitStateEvent?.Invoke();
        }

        private async Task InitState()
        {
            try
            {
                UIEventDocker.OnLoadingUIShown?.Invoke();
                await InitWaiter();
                await LoadGameScene();
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                throw;
            }
        }

        private async Task LoadGameScene()
        {
            try
            {
                await _sceneLoader.LoadScene(new LoadSceneRequest()
                {
                    SceneName = PersistantScenes.MAIN_GAME_SCENE,
                    LoadSceneMode = LoadSceneMode.Single
                }, OnSceneLoadedCallback);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                throw;
            }
           
        }

        private void OnSceneLoadedCallback()
        {
            StateMachine.SetState<CORE_GameMenuState>();
            _sceneLoader.UnloadScene(new UnloadSceneRequest()
            {
                SceneName = PersistantScenes.ENTRY_POINT_SCENE,
            });
        }

        private async Task InitWaiter()
        {
            try
            {
                //TODO: Fix Magic number
                GameInitWaiter dummyWaiter = new GameInitWaiter(5000);
                await dummyWaiter.WaitForGameLoad();
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                throw;
            }
        }

        private class GameInitWaiter
        {
            private int _waitTime;
            public GameInitWaiter(int time) => _waitTime = time;
            public async Task WaitForGameLoad() => await Task.Delay(_waitTime);
        }
    }
}
