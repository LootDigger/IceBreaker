namespace Core.ShipControls.SM
{
   public interface IStateDeprecated
   {
      public ShipStateMachine MachineReference { get; set; }
      public IControlDriver Init(ShipStateMachine _reference);
      public void Update();
   }
}
