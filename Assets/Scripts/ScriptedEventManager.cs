using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedEventManager : MonoBehaviour
{
    [SerializeField] private PlayerScriptable _player;
    [SerializeField] private StalkerScriptable _stalker;
    [SerializeField] private WallsScriptable _wallTiles;
    [SerializeField] private SoundEffectsManager _soundEffect;
    [SerializeField] private GameManager _gameManager;

    [SerializeField] private ScriptedEventSO[] _scriptedEvents;

    // TODO: This implies no two events can be running at the same time
    //       Add a check for this if needed
    private ScriptedEventSO _currentEvent;

    private float _currentEventTimer;

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

    private void Update()
    {
        _currentEventTimer += Time.deltaTime;
    }

    private void HandleScriptedEvent(ScriptedEventSO scriptedEvent)
    {
        _currentEventTimer = 0;
        _currentEvent = scriptedEvent;
        if (scriptedEvent.IsPlayerMovementDisabled)
        {
            _player.TakeControl();
        }
        if (scriptedEvent.ShouldControlPlayerMovement)
        {
            ControlPlayerMovement(scriptedEvent);
        }
        if (scriptedEvent.StalkerNotHostile)
        {
            _stalker.SetNonHostile();
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
        if (scriptedEvent.ShouldControlDoors && !scriptedEvent.ControlDoorsOnComplete)
        {
            ControlDoors(scriptedEvent);
        }
        if (scriptedEvent.ShouldChangeBackgroundMusic && !scriptedEvent.ShouldChangeBackgroundMusicOnComplete)
        {
            ChangeBackgroundMusic(scriptedEvent);
        }
        if (!scriptedEvent.ShouldControlStalkerMovement)
        {
            StalkerEventComplete();
        }
        if (scriptedEvent.ShouldPlayStalkerSoundEffectAtStart)
        {
            PlaySoundEffectAtStart(scriptedEvent, scriptedEvent.StalkerStartSoundEffect);
        }
        if (scriptedEvent.ShouldPlayStepsSoundEffectAtStart)
        {
            PlaySoundEffectAtStart(scriptedEvent, scriptedEvent.StalkerStepsSoundEffect);
        }
    }

    private void PlaySoundEffectAtStart(ScriptedEventSO scriptedEvent, AudioClip audioClip)
    {
        _soundEffect.PlaySoundEffect(audioClip, scriptedEvent.StalkerStart);
    }

    private void ChangeBackgroundMusic(ScriptedEventSO scriptedEvent)
    {
        BackgroundMusicManager.Instance.ChangeBackgroundMusic(scriptedEvent.NewBackgroundMusic);
    }

    private void ControlPlayerMovement(ScriptedEventSO scriptedEvent)
    {
        _player.SetPath(scriptedEvent.PlayerStart, scriptedEvent.PlayerEnd, scriptedEvent.PlayerTime);
    }

    private void ControlStalkerMovement(ScriptedEventSO scriptedEvent)
    {
        _stalker.SetPath(scriptedEvent.StalkerStart, scriptedEvent.StalkerEnd, scriptedEvent.StalkerSpeed);
    }

    private void StalkerEventComplete()
    {
        if (_currentEvent.HasDelayOnComplete)
        {
            StartCoroutine(DelayComplete(_currentEvent));
        }
        else
        {
            CompleteEvent(_currentEvent);
        }
    }

    private IEnumerator DelayComplete(ScriptedEventSO scriptedEvent)
    {
        yield return new WaitForSeconds(scriptedEvent.DelayTime);
        CompleteEvent(scriptedEvent);
    }

    private void CompleteEvent(ScriptedEventSO scriptedEvent)
    {
        _player.ReturnControl();
        if (_currentEvent.LightsOffOnComplete)
        {
            _player.LightsOff();
        }
        if (_currentEvent.StalkerOnComplete == StalkerScriptedEventCompleteResponse.Chase)
        {
            _stalker.StartChase();
        }
        else if (_currentEvent.StalkerOnComplete == StalkerScriptedEventCompleteResponse.Reset)
        {
            _stalker.ResetPosition();
        }
        else if (_currentEvent.StalkerOnComplete == StalkerScriptedEventCompleteResponse.Hold)
        {
            _stalker.HoldPosition(scriptedEvent.StalkerHoldPosition);
        }
        if (scriptedEvent.ControlDoorsOnComplete)
        {
            ControlDoors(scriptedEvent);
        }
        if (scriptedEvent.ShouldChangeBackgroundMusicOnComplete)
        {
            BackgroundMusicManager.Instance.ChangeBackgroundMusic(scriptedEvent.NewBackgroundMusic);
        }
        if (scriptedEvent.ShouldStopBackgroundMusic)
        {
            BackgroundMusicManager.Instance.StopMusic();
        }
        if (scriptedEvent.LinkedEvent != null)
        {
            HandleLinkedEvent(scriptedEvent);
        }
        if (scriptedEvent.EndGameOnEventComplete)
        {
            _gameManager.EndGame();
        }
        Debug.Log("Current Event Timer: " + _currentEventTimer);
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

    private void ControlDoors(ScriptedEventSO scriptedEvent)
    {
        for (int i = 0; i < scriptedEvent.AffectedDoors.Length; i++)
        {
            string doorName = scriptedEvent.AffectedDoors[i];
            Door door = GameObject.Find(doorName).GetComponent<Door>();
            if (door != null && scriptedEvent.SetDoorsOpen)
            {
                door.OpenDoor();
            }
        }
    }

    private void HandleLinkedEvent(ScriptedEventSO scriptedEvent)
    {
        _currentEvent = null;
        scriptedEvent.LinkedEvent.Event.Invoke(scriptedEvent.LinkedEvent);
    }
}
