using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDeathEventChannel", menuName = "ScriptableObjects/EventChannels/PlayerDeathEventChannel")]
public class PlayerDeathEventChannel : AbstractEventChannel<PlayerDeathEvent>
{
    
}

[System.Serializable]
public struct PlayerDeathEvent
{

}

