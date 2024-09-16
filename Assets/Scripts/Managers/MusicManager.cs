using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioClip _defaultMusic;
    [SerializeField] private float _defaultMusicVolume;

    private AudioSource _currentAudioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        PlayMusic(_defaultMusic, transform, _defaultMusicVolume);
    }

    public void PlayMusic(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        // Stop previous playing music
        if (_currentAudioSource != null)
        {
            Destroy(_currentAudioSource.gameObject, 2f);
        }

        // Spawn GameObject
        _currentAudioSource = Instantiate(_musicSource, spawnTransform.position, Quaternion.identity);

        // Assign the AudioClip, volume, and play
        _currentAudioSource.clip = audioClip;
        _currentAudioSource.volume = volume;
        _currentAudioSource.Play();
    }
}
