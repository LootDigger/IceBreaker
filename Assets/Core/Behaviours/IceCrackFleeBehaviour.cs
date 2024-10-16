using System.Collections.Generic;
using System.Linq;
using Core.Jobs;
using UnityEngine;
using UnityEngine.Jobs;
using Unity.Mathematics;
using Unity.Burst;
using UnityEngine.Splines;

namespace Core.Behaviours
{
    public class IceCrackFleeBehaviour : MonoBehaviour
    {
        [SerializeField] private float iceCrackSpeed = 1f;
        [SerializeField] private float maxSpeedMultiplyer;
        [SerializeField] private float collisionDistance = 3f;
        
        private IceBehaviourManager _behaviourManager;
        
        private void Start() => _behaviourManager = FindObjectOfType<IceBehaviourManager>();
        
        void Update()
        {
            for (int i = 0; i < _behaviourManager.GlobalShipEdges.Count(); i++)
            {
                IceCrackFleeBehaviourJob job = new IceCrackFleeBehaviourJob()
                {
                    shipEdge = _behaviourManager.GlobalShipEdges[i],
                    iceCrackSpeed = iceCrackSpeed,
                    collisionDistance = collisionDistance,
                    deltaTime = Time.deltaTime,
                    speedMultiplierRule = new Vector3(collisionDistance, 0f, maxSpeedMultiplyer)
                };
                var handle = job.Schedule(_behaviourManager.ParticlesAccessTransforms);
                handle.Complete();
            }
        }
    }
}
