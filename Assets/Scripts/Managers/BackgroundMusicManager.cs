using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    private static BackgroundMusicManager _instance;
    public static BackgroundMusicManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<BackgroundMusicManager>();
            }
            return _instance;
        }
    }

    [SerializeField] private AudioSource audioSource;

    // Make sure there is only one instance
    private void Awake()
    {
        // Check if another instance exists and destroy it
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
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
        audioSource.enabled = true;
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }
}
