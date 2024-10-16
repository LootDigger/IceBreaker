using System;
using Patterns.AbstractStateMachine;
using System.Threading.Tasks;
using Patterns.ServiceLocator;
using UnityEngine;

namespace CORE.GameStates
{
    public class CORESM_Init : AbstractState
    {
        private GameInitWaiter _gameInitWaiter;
        
        private void Awake()
        {
            ServiceLocator.RegisterService(this);
        }

        public override void EnterState()
        {
            Debug.Log("Enter State Init");
            base.EnterState();
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
            GameManagerController.GetInstance().SetGameState(ServiceLocator.GetService<CORESM_WaitForGame>());
        }

        private class GameInitWaiter
        {
            private int waitTime;
            public GameInitWaiter(int time) => waitTime = time;
            public async Task WaitForGameLoad() => await Task.Delay(waitTime);
        }
    }
}
