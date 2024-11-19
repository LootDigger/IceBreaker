using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Scene_Management
{
    public class SceneLoader
    {
        public async Task LoadScene(LoadSceneRequest request)
        {
            var operation = SceneManager.LoadSceneAsync(request.SceneName, request.LoadSceneMode);
            while (!operation.isDone)
            {
                await Task.Yield();
            }
        }
        
        public async Task LoadScene(LoadSceneRequest request, Action<Scene,LoadSceneMode> callback)
        {
            SceneManager.sceneLoaded += callback.Invoke;
            
            var operation = SceneManager.LoadSceneAsync(request.SceneName, request.LoadSceneMode);
            while (!operation.isDone)
            {
                await Task.Yield();
            }
        }
    }
}