using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedEventManager : MonoBehaviour
{
    [SerializeField] private PlayerScriptable _player;
    [SerializeField] private StalkerScriptable _stalker;
    [SerializeField] private WallsScriptable _wallTiles;

    [SerializeField] private ScriptedEventSO[] _scriptedEvents;

    // TODO: This implies no two events can be running at the same time
    //       Add a check for this if needed
    private ScriptedEventSO _currentEvent;

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
        _currentEvent = scriptedEvent;
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
        if (scriptedEvent.ShouldDestroyTiles)
        {
            _wallTiles.DestroyTiles(scriptedEvent.DestroyTiles, scriptedEvent.ShouldPlayParticleSystemOnDestroy);
        }
        if (scriptedEvent.ShouldControlSirens)
        {
            ControlSirens(scriptedEvent);
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
        if (_currentEvent.StalkerOnComplete == StalkerScriptedEventCompleteResponse.Chase)
        {
            _stalker.StartChase();
        }
        else if (_currentEvent.StalkerOnComplete == StalkerScriptedEventCompleteResponse.Reset)
        {
            _stalker.ResetPosition();
        }
    }

    private void ControlSirens(ScriptedEventSO scriptedEvent)
    {
        for (int i = 0; i < scriptedEvent.AffectedSirens.Length; i++)
        {
            string sirenName = scriptedEvent.AffectedSirens[i];
            Siren siren = GameObject.Find(sirenName).GetComponent<Siren>();
            siren.ToggleSiren(scriptedEvent.SetSirensActive);
        }
    }
}
