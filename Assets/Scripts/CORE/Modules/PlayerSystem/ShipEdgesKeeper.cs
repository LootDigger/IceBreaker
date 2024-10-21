using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

namespace CORE.Systems.PlayerSystem
{
    public class ShipEdgesKeeper : MonoBehaviour
    {
        [SerializeField] 
        private SplineContainer _splineContainer;
    
        private IEnumerable<BezierKnot> _shipEdges;
        private float3[] _globalShipEdges;
    
        public float3[] GlobalShipEdges => _globalShipEdges;

        private void Start()
        {
            Init();
        }
    
        private void Update() => UpdateShipEdgePositions();
    
        void Init() => _shipEdges = _splineContainer.Splines[0].Knots;

        private void UpdateShipEdgePositions() => _globalShipEdges = TransformToGlobalPos(GetLocalShipEdges());
    
    
        private float3[] GetLocalShipEdges() => _shipEdges.Select(knot => knot.Position).ToArray();
    
        private float3[] TransformToGlobalPos(float3[] localPositions)
        {
            _globalShipEdges = new float3[_shipEdges.Count()];
            for (int i = 0; i < localPositions.Length; i++)
            {
                localPositions[i] = _splineContainer.transform.TransformPoint(localPositions[i]);
            }
            return localPositions;
        }

    }
}
