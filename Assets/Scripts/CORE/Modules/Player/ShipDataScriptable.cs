using UnityEngine;

namespace CORE.Modules.Player
{
    [CreateAssetMenu(fileName = "ShipData",menuName = "ScriptableObjects/StaticData/ShipData")]
    public class ShipDataScriptable : ScriptableObject
    {
       [SerializeField]
       private float _shipSpeed = 10f;
       
       [SerializeField]
       private float _autopilotDuration = 2f;
       
       [SerializeField]
       private float _damageFreezeTime = 1.5f;
       
       public float ShipSpeed => _shipSpeed;
       public float AutopilotDuration => _autopilotDuration;
       public float DamageFreezeTime => _damageFreezeTime;
    }
}
