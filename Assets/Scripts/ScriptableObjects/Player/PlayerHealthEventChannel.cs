using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PlayerHealthEventChannel", menuName = "ScriptableObjects/EventChannels/PlayerHealthEventChannel")]
public class PlayerHealthEventChannel : AbstractEventChannel<PlayerHealthEvent>
{
}

[System.Serializable]
public struct PlayerHealthEvent
{
    public float MaxHP { get; private set; }
    public float CurrentHP { get; private set; }

    public PlayerHealthEvent(float maxHP, float currentHP)
    {
        MaxHP = maxHP;
        CurrentHP = currentHP;
    }
}

