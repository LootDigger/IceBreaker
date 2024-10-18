using System;
using Patterns.AbstractStateMachine;
using System.Threading.Tasks;
using CORE.GameStates.Commands;
using Patterns.Command;
using Patterns.ServiceLocator;
using UnityEngine;

namespace CORE.GameStates
{
    public class CORE_InitState : IAbstractState
    {
        private GameInitWaiter _gameInitWaiter;

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
            CommandExecuter.ExecuteCommand(new SetWaitForGameStateCommand());
        }

        private class GameInitWaiter
        {
            private int waitTime;
            public GameInitWaiter(int time) => waitTime = time;
            public async Task WaitForGameLoad() => await Task.Delay(waitTime);
        }
    }
}
