using System;
using Patterns.ServiceLocator;
using UnityEngine;

namespace CORE.Systems.PlayerSystem
{
    public class MapEdgeDetector : MonoBehaviour
    {
        [SerializeField]
        private Transform _shipTransform;
        [SerializeField]
        private float _edgeDetectionRayDistance = 20f;
        
        public Action OnMapEdgeReached;

        private void Awake()
        {
            ServiceLocator.RegisterService(this);
        }

        private void Update()
        {
            if (IsLevelEdgeDetected())
            {
                OnMapEdgeReached?.Invoke();
            }
        }

        public bool IsLevelEdgeDetected()
        {
            LayerMask mask  = LayerMask.GetMask("LevelEdge");
            Ray ray = new Ray(_shipTransform.position, _shipTransform.forward);
            Debug.DrawRay(_shipTransform.position, _shipTransform.forward);
            RaycastHit _currentHit;
            if (Physics.Raycast(ray, out _currentHit, _edgeDetectionRayDistance,mask))
            {
                return true;
            }
            return false;
        }
    }
}
