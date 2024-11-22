using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine.Jobs;

namespace Core.Jobs
{
    [BurstCompile]
    public struct IceCrackFleeBehaviourJob : IJobParallelForTransform
    {
        [ReadOnly]
        public float3 shipEdge;
        [ReadOnly]
        public float iceCrackSpeed;
        [ReadOnly]
        public float collisionDistance;
        [ReadOnly]
        public float deltaTime;
        [ReadOnly]
        public float3 speedMultiplierRule;
    
        public void Execute(int index, TransformAccess transform)
        {
            if(!transform.isValid) { return; }

            float distance = math.distance(transform.position, shipEdge);
            float normalizedDistance = math.unlerp(speedMultiplierRule.x, speedMultiplierRule.y, distance);
            normalizedDistance = math.clamp(normalizedDistance, 0f, 1f);

            if (distance > collisionDistance) { return; }

            float3 oppositeMoveVector = (float3)transform.position - shipEdge;
            oppositeMoveVector.y = 0f;
            oppositeMoveVector = math.normalize(oppositeMoveVector);

            float speedMultiplyer = math.lerp(1f, speedMultiplierRule.z, normalizedDistance);
            float3 newPos = (float3)transform.position + oppositeMoveVector * iceCrackSpeed * speedMultiplyer * deltaTime;
            transform.position = newPos;
        
        }
    }
}
