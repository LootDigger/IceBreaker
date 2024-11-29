using System;
using Cysharp.Threading.Tasks;
using Patterns.AbstractStateMachine;
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

        private async UniTask InitState()
        {
            try
            {
                UIEventDocker.OnLoadingUIShown?.Invoke();
                // await InitWaiter();
                await LoadGameScene();
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                throw;
            }
        }

        private async UniTask LoadGameScene()
        {
            try
            {
                await _sceneLoader.LoadScene(new LoadSceneRequest()
                {
                    SceneName = PersistantScenes.MAIN_GAME_SCENE,
                    LoadSceneMode = LoadSceneMode.Single
                });
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                throw;
            }
           
        }

        private void OnSceneLoadedCallback()
        {
            _sceneLoader.UnloadScene(new UnloadSceneRequest()
            {
                SceneName = PersistantScenes.ENTRY_POINT_SCENE,
            });
        }

        // private async UniTask InitWaiter()
        // {
        //     try
        //     {
        //         //TODO: Fix Magic number
        //         GameInitWaiter dummyWaiter = new GameInitWaiter(5000);
        //         await dummyWaiter.WaitForGameLoad();
        //     }
        //     catch (Exception e)
        //     {
        //         Debug.LogError(e.Message);
        //         throw;
        //     }
        // }

        // private class GameInitWaiter
        // {
        //     private int _waitTime;
        //     public GameInitWaiter(int time) => _waitTime = time;
        //     public async UniTask WaitForGameLoad() => await UniTask.Delay(_waitTime);
        // }
    }
}
