using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene_Management
{
    public class SceneLoader
    {
        private AsyncOperation _currentOperation;
        
        public async UniTask LoadScene(LoadSceneRequest request, Action callback = null, bool allowSceneActivation = true,float operationDelay = 0.0f)
        {
            if (operationDelay > 0.0f)
            {
                await UniTask.Delay((int)operationDelay * 1000);
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
                await UniTask.Yield();
            }
        }

        public void FinishSceneLoading()
        {
            if(_currentOperation == null || _currentOperation.isDone) return;
            _currentOperation.allowSceneActivation = true;
        }

        public async UniTask UnloadScene(UnloadSceneRequest request,float operationDelay = 0.0f)
        {
            if (operationDelay > 0.0f)
            {
                int time = (int)operationDelay * 1000;
                await UniTask.Delay(time);
            }
            await SceneManager.UnloadSceneAsync(request.SceneName);
        }
    }
}
