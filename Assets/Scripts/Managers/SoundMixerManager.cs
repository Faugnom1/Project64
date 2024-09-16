using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    public static SoundMixerManager Instance { get; private set; }

    [SerializeField] private AudioMixer _audioMixer;

    public void SetMasterVolume(float level)
    {
        // _audioMixer.SetFloat("MasterVolume", level);
        // Mathf.Log10(level) * 20f changes volume from logarithmic to linear interpretation
        _audioMixer.SetFloat("MasterVolume", Mathf.Log10(level) * 20f);
    }

    public void SetSoundEffectsVolume(float level)
    {
        _audioMixer.SetFloat("SoundEffectsVolume", Mathf.Log10(level) * 20f);
    }

    public void SetMusicVolume(float level)
    {
        _audioMixer.SetFloat("MusicVolume", Mathf.Log10(level) * 20f);
    }
}
