using System;
using Core.Jobs;
using CORE.Modules.IceBehaviourSystem;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Jobs;

public class IceCrackWaveReactBehaviour : MonoBehaviour
{
    [SerializeField] private IceBehaviourManager _behaviourManager;
    [SerializeField] private float collisionDistance = 3f;
    [SerializeField] private float waveHeight;
    
    private void Update()
    {
        if(!_behaviourManager.IsInitialized) {return;}
        
        float3[] edges = _behaviourManager.GetShipEdges();
        if(edges == null) { return; }

        for (int i = 0; i < edges.Length; i++)
        {
            IceCrackWaveJob waveJob = new IceCrackWaveJob()
            {
                shipEdge = edges[i],
                collisionDistance = collisionDistance,
                waveHeight = waveHeight
            };
            var waveHandle = waveJob.Schedule(_behaviourManager.ParticlesAccessTransforms);
            waveHandle.Complete();
        }
    }
}
