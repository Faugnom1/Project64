using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    public static BackgroundMusicManager instance;
    private AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Keeps music playing across screens
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Set the background music volume
    public void SetMusicVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;  // Only adjusts the music volume
        }
        else
        {
            Debug.LogError("AudioSource not assigned in BackgroundMusicManager.");
        }
    }

    public void ChangeBackgroundMusic(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }
}
