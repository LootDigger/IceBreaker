using CORE.Systems.PlayerSystem;
using Cysharp.Threading.Tasks;
using Helpers.Prefabs;
using Patterns.ServiceLocator;
using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Jobs;

namespace CORE.Modules.IceBehaviourSystem
{
    [BurstCompile]
    public class IceBehaviourManager : MonoBehaviour, ILoader
    {
        [SerializeField] 
        private ShipEdgesKeeper _shipEdgesKeeper;
        
        private IFactoryObjectsKeeper<Transform> _particlesKeeper;

        public TransformAccessArray ParticlesAccessTransforms { get; private set; }
        
        private void OnDestroy()
        {
            ParticlesAccessTransforms.Dispose();
        }

        public async UniTask Init()
        {
            _particlesKeeper = ServiceLocator.GetService<IceFactoryObjectsKeeper>();
            ParticlesAccessTransforms = new TransformAccessArray(_particlesKeeper.GetObjects());
            IsInitialized = true;
        }

        public bool IsInitialized { get; set; }

        public float3[] GetShipEdges() => _shipEdgesKeeper.GlobalShipEdges;
    }
}