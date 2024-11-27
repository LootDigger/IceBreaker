using System;
using System.Collections;
using System.Collections.Generic;
using CORE.Modules.Player;
using CORE.Modules.Procedural;
using CORE.Modules.ProceduralSystem;
using Patterns.ServiceLocator;
using UnityEditor;
using UnityEngine;
using Core.Procedural.Pooling;
using Cysharp.Threading.Tasks;
using Helpers.Prefabs;

namespace Core.Procedural.World
{
    public class ChunkGridController : MonoBehaviour, ILoader
    {
        [SerializeField]
        private GameObject _chunkPrefab;
        [SerializeField] 
        private List<Chunk> _chunks;
        [SerializeField] 
        private List<ChunkEnemyGenerator> _chunkEnemyGenerators;
        [SerializeField] 
        private List<ChunkParticlesGenerator> _chunkParticlesGenerators;
        [SerializeField]
        private float _zoneWidth;
        [SerializeField]
        private Vector2 _chunkGridSize;
        [SerializeField]
        private Collider _nonSpawnArea;
        
        public bool IsInitialized { get; set; }


        public async UniTask Init()
        {
            int deltaTimeMil = (int)Time.deltaTime * 1000;
            Debug.Log("DeltaTimeMil");
            GameObject player = ServiceLocator.GetService<Player>().gameObject;
            PoolManager poolManager = ServiceLocator.GetService<PoolManager>();
            IcebergPrefabFactory icebergFactory = ServiceLocator.GetService<IcebergPrefabFactory>();
            IcePrefabFactory iceFactory = ServiceLocator.GetService<IcePrefabFactory>();

            for (int i = 0; i < _chunks.Count; i++)
            {
                _chunks[i].Init(player);
                _chunkEnemyGenerators[i].Init(poolManager,icebergFactory);
                _chunkParticlesGenerators[i].Init(poolManager,iceFactory);
                await UniTask.Delay(deltaTimeMil);
            }

            IsInitialized = true;
        }


        private void Update()
        {
            for (int i = 0; i < _chunks.Count; i++)
            {
                _chunks[i].UpdateChunkRoutine();
            }
        }

        private void OnDrawGizmos()
        {
            Vector3 initialCalcPointBias = new(_zoneWidth * _chunkGridSize.x/2, 0f, _zoneWidth * _chunkGridSize.y/2);
            Vector3 initPoint = Vector3.zero - initialCalcPointBias;
            Gizmos.DrawSphere(initPoint,1f);
        }

#if UNITY_EDITOR

        [Sirenix.OdinInspector.Button]
        private void ClearChunkList()
        {
            _chunks.Clear();
        }
        
        [Sirenix.OdinInspector.Button]
        private void SpawnZones()
        {
            foreach (var obj in _chunks)
            {
                DestroyImmediate(obj.gameObject);
            }

            ClearChunkList();
            
            Vector3 initialCalcPointBias = new Vector3(_zoneWidth * _chunkGridSize.x/2, 0f, _zoneWidth * _chunkGridSize.y/2);
            Vector3 initPoint = Vector3.zero - initialCalcPointBias;
        
            for (int x = 0; x < _chunkGridSize.x; x++)
            {
                for (int z = 0; z < _chunkGridSize.y; z++)
                {
                    Vector3 position = new Vector3(x * _zoneWidth, 0, z * _zoneWidth);
                  //  var chunk = PrefabUtility.InstantiatePrefab(_chunkPrefab, initPoint + position, Quaternion.identity, transform).GetComponent<Chunk>();
                    GameObject spawnedChunkPrefab = PrefabUtility.InstantiatePrefab(_chunkPrefab, transform) as GameObject;
                    var chunk = spawnedChunkPrefab.GetComponent<Chunk>();
                    chunk.transform.position = initPoint + position;
                    chunk.transform.parent = transform;
                    var generator = spawnedChunkPrefab.GetComponent<ChunkEnemyGenerator>();
                    generator.InjectNonSpawnCollider(_nonSpawnArea);
                    _chunks.Add(chunk);
                }
            }
        }
        
#endif
    }
}
