namespace Core.ShipControls.SM
{
   public interface IState
   {
      public ShipStateMachine MachineReference { get; set; }
      public IMoveModule Init(ShipStateMachine _reference);
      public void Update();
   }
}
