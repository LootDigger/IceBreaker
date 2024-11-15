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
       
       public float ShipSpeed => _shipSpeed;
       public float AutopilotDuration => _autopilotDuration;
    }
}
