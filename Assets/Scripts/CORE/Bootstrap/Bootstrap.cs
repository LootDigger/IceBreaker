using System;
using System.Collections.Generic;
using CORE.Gameplay;
using CORE.GameStates;
using CORE.Modules.Player;
using CORE.Modules.ProceduralSystem;
using Helpers.Materials;
using Helpers.PrefabFabric;
using Helpers.Prefabs;
using Helpers.Prefabs.Iceberg;
using Helpers.ResourceManagement;
using Patterns.Command;
using Patterns.ServiceLocator;
using Scene_Management;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CORE.Bootstrap
{
    public class Bootstrap : SerializedMonoBehaviour
    {
        [SerializeField] 
        private readonly List<IResourcePathProvider> _icePrefabVariants = new();
        [SerializeField] 
        private readonly List<IResourcePathProvider> _icebergPrefabVariants = new();
        [SerializeField] 
        private ShipDataScriptable _shipData;
        [SerializeField] 
        private ChunkGenerationSettings _chunkGenerationSettings;

        private PrefabInstantiator _instantiator;
        private PrefabResourceLoader _resourceLoader;
        private MaterialInstanceProvider _materialInstanceProvider;

        private void Awake() => Init();

        private void Init()
        {
            CreatePersistantServices();
            RegisterCommandExecuter();
            RegisterIcePrefabFactory();
            RegisterIcebergPrefabFactory();
            RegisterServices();
            InitCoreStateMachine();
        }

        private void CreatePersistantServices()
        {
            _instantiator = new PrefabInstantiator();
            _resourceLoader = new PrefabResourceLoader();
            _materialInstanceProvider = new MaterialInstanceProvider();
        }

        private void RegisterServices()
        {
            ServiceLocator.RegisterService(new SceneLoader());
            ServiceLocator.RegisterService(new ShipStaticDataProvider(_shipData));
        }

        private void RegisterCommandExecuter()
        {
            var commandExecuter = new CommandExecuter();
            ServiceLocator.RegisterService(commandExecuter);
        }

        private void InitCoreStateMachine()
        {
            CoreStateMachine stateMachine = new CoreStateMachine();
            ServiceLocator.RegisterService(stateMachine);
            stateMachine.SetState<CORE_InitState>();
        }
        
        
        // TODO: Create method for factory creation
        private void RegisterIcePrefabFactory()
        {
            IceParticlesVariantsProvider prefabsProvider = new();
            IceFactoryObjectsKeeper objectsKeeper = new(maxCount:_chunkGenerationSettings.GlobalParticlesCount);
            foreach (var variant in _icePrefabVariants)
            {
                prefabsProvider.AddVariant(variant);
            }
            IcePrefabFactory icePrefabFactory = new(_instantiator, _resourceLoader, prefabsProvider,_materialInstanceProvider,objectsKeeper);
            ServiceLocator.RegisterService(icePrefabFactory);
            ServiceLocator.RegisterService(objectsKeeper);
        }
        
        private void RegisterIcebergPrefabFactory()
        {
            IcebergVariantsProvider prefabsProvider = new();
            IcebergFactoryObjectsKeeper objectsKeeper = new();
            foreach (var variant in _icebergPrefabVariants)
            {
                prefabsProvider.AddVariant(variant);
            }
            IcebergPrefabFactory icePrefabFactory = new(_instantiator, _resourceLoader, prefabsProvider,_materialInstanceProvider,objectsKeeper);
            ServiceLocator.RegisterService(icePrefabFactory);
            ServiceLocator.RegisterService(prefabsProvider);
        }
    }
}
