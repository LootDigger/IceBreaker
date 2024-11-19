using System;
using System.Collections;
using System.Collections.Generic;
using Patterns.ServiceLocator;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]
    private AnimationInvoker _shipDamageAnimation;
    
    [SerializeField]
    private AnimationInvoker _shipSinkAnimation;
    
    public AnimationInvoker ShipDamageAnimation => _shipDamageAnimation;
    public AnimationInvoker ShipSinkAnimation => _shipSinkAnimation;
    
    private void Awake()
    {
        ServiceLocator.RegisterService(this);
    }
}
