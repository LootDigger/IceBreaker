using UnityEngine;

public class AppFrameRateSetter : MonoBehaviour
{
    void Awake()
    {
        Application.targetFrameRate = 30;
    }

}
