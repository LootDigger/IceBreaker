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
        
        private TransformAccessArray _particlesAccessTransforms;
        private IFactoryObjectsKeeper<Transform> _particlesKeeper;

        public TransformAccessArray ParticlesAccessTransforms
        {
            get
            {
                _particlesAccessTransforms.SetTransforms(_particlesKeeper.GetObjects());
                return _particlesAccessTransforms;
            }
        }
        
        private void Start() => Init();

        private void OnDestroy()
        {
            _particlesAccessTransforms.Dispose();
        }

        private void Init()
        {
            _particlesAccessTransforms = new TransformAccessArray(new Transform[1]);
            _particlesKeeper = ServiceLocator.GetService<IceFactoryObjectsKeeper>();
        }

        public float3[] GetShipEdges() => _shipEdgesKeeper.GlobalShipEdges;
    }
}