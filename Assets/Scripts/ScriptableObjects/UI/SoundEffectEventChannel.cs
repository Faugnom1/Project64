using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundEffectEventChannel", menuName = "ScriptableObjects/EventChannels/SoundEffectEventChannel")]
public class SoundEffectEventChannel : AbstractEventChannel<SoundEffectEvent>
{

}

public struct SoundEffectEvent
{
    public AudioClip AudioClip { get; private set; }

    public SoundEffectEvent(AudioClip audioClip) { AudioClip = audioClip; }
}
