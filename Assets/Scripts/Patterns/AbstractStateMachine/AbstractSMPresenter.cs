using System.Collections;
using System.Collections.Generic;
using Patterns.AbstractStateMachine;
using Patterns.ServiceLocator;
using UnityEngine;

public abstract class AbstractSMPresenter : MonoBehaviour
{
    private StateMachine _stateMachine;

    protected void Start()
    {
        _stateMachine = new();
        InitSM();
    }
    
    protected void Update()
    {
        _stateMachine.UpdateMachine();
    }

    private void InitSM()
    {
        _stateMachine = new StateMachine();
    }

    public void SetState(IAbstractState state)
    {
        _stateMachine.SetState(state);
    }
}
