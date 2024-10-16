using UnityEngine;

namespace Core.ShipControls.SM
{
    public class ShipStateMachine
    {
        private IState _currentState = null;
        public System.Action<IMoveModule> OnStateChanged;
        public AutoPilotSettings APSettings { get; private set; }
        public ManualPilotSettings MPSettings { get; private set; }

        public ShipStateMachine(AutoPilotSettings _apSettings, ManualPilotSettings _mpSettings)
        {
            this.APSettings = _apSettings;
            this.MPSettings = _mpSettings;
        }

        public void InitMachine() => SetState(new ManualPilotState());
    
        public void UpdateMachine() => _currentState.Update();

        public void SetState(IState newState)
        {
            _currentState = newState;
            OnStateChanged?.Invoke(_currentState.Init(this));
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
