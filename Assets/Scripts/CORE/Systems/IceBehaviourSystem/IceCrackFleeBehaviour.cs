using Core.Jobs;
using CORE.Systems.IceBehaviourSystem;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Jobs;
using UnityEngine.Serialization;

namespace Core.Behaviours
{
    public class IceCrackFleeBehaviour : MonoBehaviour
    {
        [SerializeField] private IceBehaviourManager _behaviourManager;
        [SerializeField] private float _iceCrackSpeed = 1.5f;
        [SerializeField] private float _maxSpeedMultiplyer = 8f;
        [SerializeField] private float _collisionDistance = 2f;
        
        void Update()
        {
            float3[] edges = _behaviourManager.GetShipEdges();
            for (int i = 0; i < edges.Length; i++)
            {
                IceCrackFleeBehaviourJob job = new IceCrackFleeBehaviourJob()
                {
                    shipEdge = edges[i],
                    iceCrackSpeed = _iceCrackSpeed,
                    collisionDistance = _collisionDistance,
                    deltaTime = Time.deltaTime,
                    speedMultiplierRule = new Vector3(_collisionDistance, 0f, _maxSpeedMultiplyer)
                };
                var handle = job.Schedule(_behaviourManager.ParticlesAccessTransforms);
                handle.Complete();
            }
        }
    }
}
