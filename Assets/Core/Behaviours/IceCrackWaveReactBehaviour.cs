using System;
using Core.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

public class IceCrackWaveReactBehaviour : MonoBehaviour
{
    [SerializeField] private float collisionDistance = 3f;
    [SerializeField] private float waveHeight;

    private IceBehaviourManager _behaviourManager;
    private void Start() => _behaviourManager = FindObjectOfType<IceBehaviourManager>();

    private void Update()
    {
        for (int i = 0; i < _behaviourManager.GlobalShipEdges.Length; i++)
        {
            IceCrackWaveJob waveJob = new IceCrackWaveJob()
            {
                shipEdge = _behaviourManager.GlobalShipEdges[i],
                collisionDistance = collisionDistance,
                waveHeight = waveHeight
            };
            var waveHandle = waveJob.Schedule(_behaviourManager.ParticlesAccessTransforms);
            waveHandle.Complete();
        }
    }
}
