using System;
using System.Collections.Generic;
using CORE.Systems.IceSpawnSystem;
using CORE.Systems.PlayerSystem;
using Patterns.ServiceLocator;
using UnityEngine;
using UnityEngine.Serialization;

namespace CORE.Systems.ProceduralSystem
{
    [RequireComponent(typeof(ChunkPropsGenerator))]
    public class Chunk : MonoBehaviour
    {
        [SerializeField]
        private float _generateDistance;
        [SerializeField]
        private float _disposeDistance;
        [SerializeField]
        private ChunkPropsGenerator _propsGenerator;
        [SerializeField]
        private ChunkEnemyGenerator _enemyGenerator;
        
        private GameObject _player;
        private bool _isGenerated;
        
        private void Awake()
        {
            _player = ServiceLocator.GetService<Player>().gameObject;
        }

        private void Update() => UpdateChunkRoutine();

        private void UpdateChunkRoutine()
        {
            if (CalculateDistanceToPlayer() < _generateDistance && !_isGenerated)
            {
                Generate();
            }
            else if (CalculateDistanceToPlayer() > _disposeDistance && _isGenerated)
            {
                Dispose();
            }
        }

        private float CalculateDistanceToPlayer()
        {
            return Vector3.Distance(_player.transform.position, transform.position);
        }

        private void Generate()
        {
            _isGenerated = true;
            _propsGenerator.Generate();
            _enemyGenerator.Generate();
        }
        
        private void Dispose()
        {            
            _isGenerated = false;
            _propsGenerator.Dispose();
            _enemyGenerator.Dispose();
        }
    }
}
