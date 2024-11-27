using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene_Management
{
    public class LoadSceneRequest: ISceneRequest,ILoadableScene
    {
       public string SceneName { get; set; }
       public LoadSceneMode LoadSceneMode { get; set; }
    }

    public class UnloadSceneRequest : ISceneRequest
    {
        public string SceneName { get; set; }
    }

    public interface ISceneRequest
    {
        public string SceneName { get; set; }
    }
    
    public interface ILoadableScene
    {
        public LoadSceneMode LoadSceneMode { get; set; }
    }
}
