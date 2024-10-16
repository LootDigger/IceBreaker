using UnityEngine;

namespace Core.ShipControls.SM
{
    public class ShipStateMachine
    {
        private IStateDeprecated _currentStateDeprecated = null;
        public System.Action<IControlDriver> OnStateChanged;
        public AutoPilotSettings APSettings { get; private set; }
        public ManualPilotSettings MPSettings { get; private set; }

        public ShipStateMachine(AutoPilotSettings _apSettings, ManualPilotSettings _mpSettings)
        {
            this.APSettings = _apSettings;
            this.MPSettings = _mpSettings;
        }

        public void InitMachine() => SetState(new ManualPilotStateDeprecated());
    
        public void UpdateMachine() => _currentStateDeprecated.Update();

        public void SetState(IStateDeprecated newStateDeprecated)
        {
            _currentStateDeprecated = newStateDeprecated;
            OnStateChanged?.Invoke(_currentStateDeprecated.Init(this));
        }

        public struct AutoPilotSettings
        {
            public float autoPilotDuration;
        }
        
        public struct ManualPilotSettings
        {
            public Transform playerTransform;
            public float edgeDetectionRayDistance;
        }
    }
}
