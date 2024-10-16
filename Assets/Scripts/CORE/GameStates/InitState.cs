using System;
using Patterns.AbstractStateMachine;
using System.Threading.Tasks;
using Patterns.ServiceLocator;

namespace CORE.GameStates
{
    public class InitState : AbstractState
    {
        private GameInitWaiter _gameInitWaiter;

        public override void EnterState()
        {
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
            GameManagerController.GetInstance().SetGameState(ServiceLocator.GetService<WaitForStartState>());
        }

        private class GameInitWaiter
        {
            private int waitTime;
            public GameInitWaiter(int time) => waitTime = time;
            public async Task WaitForGameLoad() => await Task.Delay(waitTime);
        }
    }
}
