using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Helpers.ResourceManagement
{
  [CreateAssetMenu(fileName = "ResourcePathProvider", menuName = "ScriptableObjects/Resources/Resource Path Provider", order = 1)]
  public class ResourcePathProviderScriptable : ScriptableObject, IResourcePathProvider
  {
    [SerializeField]
    private Object _targetResourceAsset;
    
    [SerializeField][HideInInspector]
    private string _targetAssetPath;
    
    private void OnValidate()
    {
      #if UNITY_EDITOR
      
      if(_targetResourceAsset == null) {return;}
      var assetPath = AssetDatabase.GetAssetPath(_targetResourceAsset);
      if (!IsResourcePath(assetPath))
      {
        _targetResourceAsset = null;
        Debug.LogWarning("Target Asset must be the Resource asset");
        return;
      }
      _targetAssetPath = TrimResourcePath(assetPath);
      
      #endif
    }

    private string TrimResourcePath(string path)
    {
      string trimmedPath = path;
      int resourcesIndex = trimmedPath.IndexOf("Resources/");
      if (resourcesIndex >= 0)
      {
        trimmedPath = trimmedPath.Substring(resourcesIndex + "Resources/".Length);
      }

      int extensionIndex = trimmedPath.LastIndexOf('.');
      if (extensionIndex >= 0)
      {
        trimmedPath = trimmedPath.Substring(0, extensionIndex);
      }
      return trimmedPath;
    }

    private bool IsResourcePath(string path)
    {
      return path.Contains("Resources/");
    }

    public string GetPath() => _targetAssetPath;
  }
}
