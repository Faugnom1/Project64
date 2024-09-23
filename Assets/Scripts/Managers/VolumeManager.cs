using UnityEngine;

public class VolumeManager : MonoBehaviour
{
    public static VolumeManager instance;

    private float masterVolume = 1f;
    private float musicVolume = 1f;
    private float sfxVolume = 1f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Keeps the volume manager across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Adjust the master volume (affects both music and SFX)
    public void SetMasterVolume(float value)
    {
        masterVolume = value;
        AudioListener.volume = masterVolume;
    }

    // Adjust only the background music volume
    public void SetMusicVolume(float value)
    {
        musicVolume = value;
        BackgroundMusicManager.Instance.SetMusicVolume(musicVolume);
    }

    public void SetSFXVolume(float value)
    {
        sfxVolume = value;
        // Here you can also update any global SFX sources if needed
    }

    public float GetMasterVolume() => masterVolume;
    public float GetMusicVolume() => musicVolume;
    public float GetSFXVolume() => sfxVolume;
}
