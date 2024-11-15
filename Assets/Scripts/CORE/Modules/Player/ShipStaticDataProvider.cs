namespace CORE.Modules.Player
{
   public class ShipStaticDataProvider
   {
      public ShipDataScriptable Data { get; private set; }

      public ShipStaticDataProvider(ShipDataScriptable data)
      {
         Data = data;
      }
   }
}
