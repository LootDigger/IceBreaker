using Patterns.AbstractStateMachine;
using Patterns.ServiceLocator;
using UnityEngine;

namespace Core.ShipControls
{
    public class ShipSMPresenter : AbstractSMPresenter
    {
        void Start()
        {
            base.Start();
            ServiceLocator.RegisterService(this);
        }
    }
}