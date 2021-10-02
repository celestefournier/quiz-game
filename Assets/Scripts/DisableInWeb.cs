using UnityEngine;

public class DisableInWeb : MonoBehaviour
{
    void Awake()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            gameObject.SetActive(false);
        };
    }
}
