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

    public void PlaySoundEffect(AudioClip audioClip, Vector2 spawnPosition, float volume = 1f)
    {
        // Make sure audio clip exists
        if (audioClip == null)
        {
            return;
        }

        // Spawn GameObject
        AudioSource audioSource = Instantiate(_soundEffect, spawnPosition, Quaternion.identity);

        // Assign the AudioClip, volume, and play
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();

        // Destroy GameObject after clip plays
        Destroy(audioSource.gameObject, audioSource.clip.length);
    }

    public void PlaySoundEffect(SoundEffectEvent soundEvent)
    {
        PlaySoundEffect(_soundEffect.clip, Vector2.one);
    }
}
