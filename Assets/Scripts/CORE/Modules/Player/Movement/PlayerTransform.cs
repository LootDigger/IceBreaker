using Patterns.ServiceLocator;
using UnityEngine;

public class PlayerTransform : MonoBehaviour
{
   [SerializeField] private Transform _rootObjectTransform;
   [SerializeField] private Transform _modelTransform;
   [SerializeField] private Transform _rotationTransform;
   
   [SerializeField] private Transform _initTransform;
   
   private void Awake()
   {
      ServiceLocator.RegisterService(this);
   }
   
   [Sirenix.OdinInspector.Button]
   public void ResetTransform()
   {
      Debug.Log("ResetTransform");
      _rootObjectTransform.position = _initTransform.position;
      _modelTransform.position = Vector3.zero;
      _rotationTransform.localRotation = _initTransform.localRotation;
   }
}
