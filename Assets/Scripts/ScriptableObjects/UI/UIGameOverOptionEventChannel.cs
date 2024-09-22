using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UIGameOverOptionEventChannel", menuName = "ScriptableObjects/EventChannels/UIGameOverOptionEventChannel")]
public class UIGameOverOptionEventChannel : AbstractEventChannel<UIGameOverOptionEvent>
{

}

public struct UIGameOverOptionEvent
{
    public GameOverOption GameOverOption { get; private set; }

    public UIGameOverOptionEvent(GameOverOption gameOverOption) {  GameOverOption = gameOverOption; }
}
