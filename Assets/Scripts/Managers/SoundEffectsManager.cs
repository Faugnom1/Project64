using UnityEngine;

public class SoundEffectsManager : MonoBehaviour
{
    public static SoundEffectsManager Instance { get; private set; }

    [SerializeField] private AudioSource _soundEffect;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void PlaySoundEffect(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        // Make sure audio clip exists
        if (audioClip == null)
        {
            return;
        }

        // Spawn GameObject
        AudioSource audioSource = Instantiate(_soundEffect, spawnTransform.position, Quaternion.identity);

        // Assign the AudioClip, volume, and play
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();

        // Destroy GameObject after clip plays
        Destroy(audioSource.gameObject, audioSource.clip.length);
    }
}
