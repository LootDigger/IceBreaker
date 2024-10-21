using CORE.Systems.PlayerSystem.SM;
using Patterns.AbstractStateMachine;

public class SHIP_GameInitState : IState
{
    public StateMachine StateMachine { get; set; }

    public SHIP_GameInitState(StateMachine stateMachine)
    {
        StateMachine = stateMachine;
    }

    public void EnterState()
    {
        StateMachine.SetState<SHIP_ManualPilotState>();
    }
}
