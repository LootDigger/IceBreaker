using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Procedural.World
{
    public class ChunkGridSpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject _chunkZone;
        [SerializeField]
        private float _zoneWidth;
        [SerializeField]
        private Vector2 _chunkGridSize;
        
        void Start()
        {
            SpawnZones();
        }

        private void OnDrawGizmos()
        {
            Vector3 initialCalcPointBias = new Vector3(_zoneWidth * _chunkGridSize.x/2, 0f, _zoneWidth * _chunkGridSize.y/2);
            Vector3 initPoint = Vector3.zero - initialCalcPointBias;
            Gizmos.DrawSphere(initPoint,1f);
        }

        private void SpawnZones()
        {
            Vector3 initialCalcPointBias = new Vector3(_zoneWidth * _chunkGridSize.x/2, 0f, _zoneWidth * _chunkGridSize.y/2);
            Vector3 initPoint = Vector3.zero - initialCalcPointBias;
        
            for (int x = 0; x < _chunkGridSize.x; x++)
            {
                for (int z = 0; z < _chunkGridSize.y; z++)
                {
                    Vector3 position = new Vector3(x * _zoneWidth, 0, z * _zoneWidth);
                    Instantiate(_chunkZone, initPoint + position, Quaternion.identity, transform);
                }
            }
        }
    }
}
