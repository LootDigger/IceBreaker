using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using Unity.Jobs;
using UnityEngine.Jobs;
using Unity.Mathematics;
using Unity.Burst;

[BurstCompile]
public class TransformManager : MonoBehaviour
{
    public Transform player;
    public GameObject agentPrefab;

    public float agentSpawnRadius = 30f;
    public int agentCount = 100;
    public float agentSpeed = 1f;
    public float playerMinDistance = 3f;
    
    private List<Transform> transforms = new List<Transform>();
    private TransformAccessArray accessTransforms;

    private void Start()
    {
        for (int i = 0; i < agentCount; i++)
        {
            
            Vector3 spawnPosition = UnityEngine.Random.insideUnitSphere * agentSpawnRadius;
            spawnPosition.y = 0.5f;
            transforms.Add(Instantiate(agentPrefab, spawnPosition, Quaternion.identity).GetComponent<Transform>());
        }
        accessTransforms = new TransformAccessArray(transforms.ToArray());
    }

    private void Update()
    {
        AgentFleeJob agentFleeJob = new AgentFleeJob()
        {
            playerPosition = player.position,
            agentSpeed = agentSpeed,
            agentMinDistance = playerMinDistance,
            deltaT = Time.deltaTime
        };
        var handle = agentFleeJob.Schedule(accessTransforms);
        
        handle.Complete();
    }

    private void OnDestroy()
    {
        accessTransforms.Dispose();
    }
}

[BurstCompile]
public struct AgentFleeJob : IJobParallelForTransform
{
    [ReadOnly]
    public float3 playerPosition;
    [ReadOnly]
    public float agentSpeed;
    [ReadOnly]
    public float agentMinDistance;
    [ReadOnly]
    public float deltaT;
    
    public void Execute(int index, TransformAccess transform)
    {
        if (math.distance(transform.position, playerPosition) > agentMinDistance) { return; }
        
        float3 oppositeMoveVector = (float3)transform.position - playerPosition;
        oppositeMoveVector.y = 0f;
        oppositeMoveVector = math.normalize(oppositeMoveVector);
        float3 newPos = (float3)transform.position + oppositeMoveVector * agentSpeed * deltaT;
        
        transform.position = newPos;
    }
}
