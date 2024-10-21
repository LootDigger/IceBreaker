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
    }
}
