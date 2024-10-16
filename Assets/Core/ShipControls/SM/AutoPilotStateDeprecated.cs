using System.Threading.Tasks;

namespace Core.ShipControls.SM
{
    public class AutoPilotStateDeprecated : IStateDeprecated
    {
        private bool _isAutoPilotRunning = false;

        public ShipStateMachine MachineReference { get; set; }

        public IControlDriver Init(ShipStateMachine _reference)
        {
            MachineReference = _reference;
            StartAutoPilotWaiter();
            return new ShipAutoPilotMovement();
        }

        private async Task StartAutoPilotWaiter()
        {
            _isAutoPilotRunning = true;
            await Task.Delay((int)MachineReference.APSettings.autoPilotDuration * 1000);
            _isAutoPilotRunning = false;
        }

        public void Update()
        {
            if (!_isAutoPilotRunning)
            {
                MachineReference.SetState(new ManualPilotStateDeprecated());
            }
        }
    }
}