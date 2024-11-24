using CORE.Systems.PlayerSystem;
using Helpers.Prefabs;
using Patterns.ServiceLocator;
using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Jobs;

namespace CORE.Modules.IceBehaviourSystem
{
    [BurstCompile]
    public class IceBehaviourManager : MonoBehaviour
    {
        [SerializeField] 
        private ShipEdgesKeeper _shipEdgesKeeper;
        
        private IFactoryObjectsKeeper<Transform> _particlesKeeper;

        public TransformAccessArray ParticlesAccessTransforms { get; private set; }
        
        private void Start() => Init();

        private void OnDestroy()
        {
            ParticlesAccessTransforms.Dispose();
        }

        private void Init()
        {
            _particlesKeeper = ServiceLocator.GetService<IceFactoryObjectsKeeper>();
            ParticlesAccessTransforms = new TransformAccessArray(_particlesKeeper.GetObjects());
        }

        public float3[] GetShipEdges() => _shipEdgesKeeper.GlobalShipEdges;
    }
}