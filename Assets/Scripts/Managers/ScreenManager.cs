using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    // Singleton instance
    public static ScreenManager Instance { get; private set; }

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // If the instance is null, set it to this instance of GameManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate ScreenManager instances
        }
    }

    private void Start()
    {
        SetWindowed();
    }

    public void SetWindowed()
    {
        Screen.fullScreenMode = FullScreenMode.Windowed;
        Screen.fullScreen = false;
    }
}
