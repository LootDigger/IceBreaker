using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    #if DEVELOPMENT_BUILD || UNITY_EDITOR
    private float deltaTime = 0.0f;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    void OnGUI()
    {
        float fps = 1.0f / deltaTime;
        string text = $"FPS: {Mathf.Ceil(fps)}";
        
        GUIStyle style = new GUIStyle();
        style.fontSize = 64;
        style.normal.textColor = Color.white;

        Rect rect = new Rect(10, 10, 200, 50);
        GUI.Label(rect, text, style);
    }
    #endif
}