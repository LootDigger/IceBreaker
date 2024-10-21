using System;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene_Management
{
    public class LoadSceneRequest 
    {
       public string SceneName { get; set; }
       public LoadSceneMode LoadSceneMode { get; set; }
    }
}
