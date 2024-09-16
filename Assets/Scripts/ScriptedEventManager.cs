using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedEventManager : MonoBehaviour
{
    [SerializeField] private PlayerScriptable _player;
    [SerializeField] private StalkerScriptable _stalker;

    [SerializeField] private ScriptedEventSO[] _scriptedEvents;

    private void OnEnable()
    {
        for (int i = 0; i < _scriptedEvents.Length; i++)
        {
            _scriptedEvents[i].Event.AddListener(HandleScriptedEvent);
        }

        _stalker.OnStalkerScriptedEventComplete.AddListener(StalkerEventComplete);
    }

    private void OnDisable()
    {
        for (int i = 0; i < _scriptedEvents.Length; i++)
        {
            _scriptedEvents[i].Event.RemoveListener(HandleScriptedEvent);
        }

        _stalker.OnStalkerScriptedEventComplete.RemoveListener(StalkerEventComplete);
    }

    private void HandleScriptedEvent(ScriptedEventSO scriptedEvent)
    {
        if (scriptedEvent.IsPlayerMovementDisabled)
        {
            _player.TakeControl();
        }
        if (scriptedEvent.ShouldControlPlayerMovement)
        {
            ControlPlayerMovement(scriptedEvent);
        }
        if (scriptedEvent.ShouldControlStalkerMovement)
        {
            ControlStalkerMovement(scriptedEvent);
        }
        if (scriptedEvent.ShouldScreenShake)
        {
            _player.StartScreenShake();
        }
    }

    private void ControlPlayerMovement(ScriptedEventSO scriptedEvent)
    {

    }

    private void ControlStalkerMovement(ScriptedEventSO scriptedEvent)
    {
        _stalker.SetPath(scriptedEvent.StalkerStart, scriptedEvent.StalkerEnd, scriptedEvent.StalkerSpeed);
    }

    private void StalkerEventComplete()
    {
        _player.ReturnControl();
        _stalker.ResetPosition();
    }
}
