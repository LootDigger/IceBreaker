using System;
using Patterns.AbstractStateMachine;
using System.Threading.Tasks;
using UnityEngine;

namespace CORE.GameStates
{
    public class CORE_InitState : IState
    {
        private GameInitWaiter _gameInitWaiter;

        public StateMachine StateMachine { get; set; }
        
        public CORE_InitState(StateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        public void EnterState()
        {
            Debug.Log("CORE: Enter State Init");
            InitWaiter();
        }

        private async Task InitWaiter()
        {
            //TODO: change to some initializer logic 
            _gameInitWaiter = new GameInitWaiter(5000);
            await WaitForGameLoad();
        }

        async Task WaitForGameLoad()
        {
            await _gameInitWaiter.WaitForGameLoad();
            StateMachine.SetState<CORE_GameMenuState>();
        }

        private class GameInitWaiter
        {
            private int waitTime;
            public GameInitWaiter(int time) => waitTime = time;
            public async Task WaitForGameLoad() => await Task.Delay(waitTime);
        }
    }
}
