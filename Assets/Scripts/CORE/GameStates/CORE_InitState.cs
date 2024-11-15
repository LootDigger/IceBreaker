using System;
using Patterns.AbstractStateMachine;
using System.Threading.Tasks;
using Scene_Management;
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
            UIEventDocker.OnLoadingUIShown.Invoke();
            await InitWaiter();
            await LoadGameScene();
        }

        private async Task LoadGameScene()
        {
            await _sceneLoader.LoadScene(new LoadSceneRequest()
            {
                SceneName = PersistantScenes.MAIN_GAME_SCENE,
                LoadSceneMode = LoadSceneMode.Single
            }, OnSceneLoadedCallback);
        }

        private void OnSceneLoadedCallback(Scene scene, LoadSceneMode mode)
        {
            StateMachine.SetState<CORE_GameMenuState>();
            SceneManager.sceneLoaded -= OnSceneLoadedCallback;
        }

        private async Task InitWaiter()
        {
            //TODO: Fix Magic number
            GameInitWaiter dummyWaiter = new GameInitWaiter(5000);
            await dummyWaiter.WaitForGameLoad();
        }

        private class GameInitWaiter
        {
            private int _waitTime;
            public GameInitWaiter(int time) => _waitTime = time;
            public async Task WaitForGameLoad() => await Task.Delay(_waitTime);
        }
    }
}
