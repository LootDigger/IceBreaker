using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene_Management
{
    public class SceneLoader
    {
        private AsyncOperation _currentOperation;
        
        public async Task LoadScene(LoadSceneRequest request, Action callback = null, bool allowSceneActivation = true,float operationDelay = 0.0f)
        {
            if (operationDelay > 0.0f)
            {
                await Task.Delay((int)operationDelay * 1000);
            }
            AsyncOperation operation = SceneManager.LoadSceneAsync(request.SceneName, request.LoadSceneMode);
            _currentOperation = operation;
            operation.allowSceneActivation = allowSceneActivation;

            if (callback != null)
            {
                operation.completed += (AsyncOperation op) => { callback.Invoke(); };
            }

            while (!operation.isDone)
            {
                await Task.Yield();
            }
        }

        public void FinishSceneLoading()
        {
            if(_currentOperation == null || _currentOperation.isDone) return;
            _currentOperation.allowSceneActivation = true;
        }

        public async Task UnloadScene(UnloadSceneRequest request,float operationDelay = 0.0f)
        {
            if (operationDelay > 0.0f)
            {
                int time = (int)operationDelay * 1000;
                await Task.Delay(time);
            }
            SceneManager.UnloadSceneAsync(request.SceneName);
        }
    }
}
