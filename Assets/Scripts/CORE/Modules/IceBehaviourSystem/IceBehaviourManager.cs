using Core.Procedural.World;
using CORE.Systems.IceSpawnSystem;
using CORE.Systems.PlayerSystem;
using CORE.Systems.ProceduralSystem;
using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Jobs;

namespace CORE.Systems.IceBehaviourSystem
{
    [BurstCompile]
    public class IceBehaviourManager : MonoBehaviour
    {
        [SerializeField] 
        private IceSpawner _spawner;
        [SerializeField] 
        private ShipEdgesKeeper _shipEdgesKeeper;
        
        private TransformAccessArray _particlesAccessTransforms;
        public TransformAccessArray ParticlesAccessTransforms => _particlesAccessTransforms;
    
        private void Start() => Init();

        private void OnDestroy()
        {
            _particlesAccessTransforms.Dispose();
            UnsubscribeEvents();
        }

        private void Init()
        {
            _particlesAccessTransforms = new TransformAccessArray(new Transform[1]);
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            ChunkPropsGenerator.onChunkGenerated += UpdateTransformAccessArray;
            ChunkPropsGenerator.onChunkDisposed += UpdateTransformAccessArray;
        }

        private void UnsubscribeEvents() 
        {
            ChunkPropsGenerator.onChunkGenerated -= UpdateTransformAccessArray;
            ChunkPropsGenerator.onChunkDisposed -= UpdateTransformAccessArray;
        }
    
        private void UpdateTransformAccessArray()
        {
            var particles = _spawner.GetAllParticles();
            _particlesAccessTransforms.SetTransforms(particles);
        }

        public float3[] GetShipEdges() => _shipEdgesKeeper.GlobalShipEdges;
    }
}