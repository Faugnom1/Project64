using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    public static SoundMixerManager Instance { get; private set; }

    [SerializeField] private AudioMixer _audioMixer;


    public void SetMasterVolume(float level)
    {
        _audioMixer.SetFloat("MasterVolume", level);
    }

    public void SetSoundEffectsVolume(float level)
    {
        _audioMixer.SetFloat("SoundEffectsVolume", level);
    }

    public void SetMusicVolume(float level)
    {
        _audioMixer.SetFloat("MusicVolume", level);
    }
}
