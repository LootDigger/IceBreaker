using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine.Jobs;

namespace Core.Jobs
{
    [BurstCompile]
    public struct IceCrackWaveJob : IJobParallelForTransform
    {
        [ReadOnly] public float3 shipEdge;

        [ReadOnly] public float waveHeight;

        [ReadOnly] public float collisionDistance;

        public void Execute(int index, TransformAccess transform)
        {
            float distance = math.distance(transform.position, shipEdge);
            float normalizedDistance = math.unlerp(0, collisionDistance, distance);

            normalizedDistance = math.clamp(normalizedDistance, 0f, 1f);

            float iceCrackHeight = math.sin(1f - normalizedDistance) * waveHeight;

            float3 newPos = transform.position;
            newPos.y = iceCrackHeight;
            transform.position = newPos;
        }
    }
}