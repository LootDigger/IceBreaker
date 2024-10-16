using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Behaviours;
using Core.Jobs;
using Core.Procedural.World;
using CORE.Systems.IceSpawnSystem;
using UnityEngine;
using UnityEngine.Jobs;
using Unity.Mathematics;
using Unity.Burst;
using UnityEngine.Splines;

[BurstCompile]
public class IceBehaviourManager : MonoBehaviour
{
    [SerializeField] private SplineContainer splineContainer;
    [SerializeField] private IceSpawner spawner;

    private TransformAccessArray _particlesAccessTransforms;
    private IEnumerable<BezierKnot> _shipEdges;
    private float3[] _globalShipEdges;

    public TransformAccessArray ParticlesAccessTransforms => _particlesAccessTransforms;
    public float3[] GlobalShipEdges => _globalShipEdges;
    
    private void Start() => Init();

    private void Update()
    {
        AssignGlobalShipEdges(TransformToGlobalPos(GetLocalShipEdges()));
    }

    private void OnDestroy()
    {
        _particlesAccessTransforms.Dispose();
        UnsubscribeEvents();
    }

    private void Init()
    {
        _particlesAccessTransforms = new TransformAccessArray(new Transform[1]);
        _shipEdges = splineContainer.Splines[0].Knots;
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        Chunk.onChunkGenerated += UpdateTransformAccessArray;
        Chunk.onChunkDisposed += UpdateTransformAccessArray;
    }

    private void UnsubscribeEvents() 
    {
        Chunk.onChunkGenerated -= UpdateTransformAccessArray;
        Chunk.onChunkDisposed -= UpdateTransformAccessArray;
    }
    
    private void UpdateTransformAccessArray()
    {
        var particles = spawner.GetAllParticles();
        _particlesAccessTransforms.SetTransforms(particles);
    }

    private float3[] GetLocalShipEdges() => _shipEdges.Select(knot => knot.Position).ToArray();

    private float3[] TransformToGlobalPos(float3[] localPositions)
    {
        _globalShipEdges = new float3[_shipEdges.Count()];
        for (int i = 0; i < localPositions.Length; i++)
        {
            localPositions[i] = splineContainer.transform.TransformPoint(localPositions[i]);
        }
        return localPositions;
    }

    private void AssignGlobalShipEdges(float3[] shipEdgesData)
    {
        for (int i = 0; i < _shipEdges.Count(); i++)
        {
            _globalShipEdges[i] = shipEdgesData[i];
        }
    }
}