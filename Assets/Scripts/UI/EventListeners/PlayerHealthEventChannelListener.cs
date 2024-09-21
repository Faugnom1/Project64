using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHPEventChannelListener : MonoBehaviour
{
    [Header("Listen to Event Channels")]
    [SerializeField] private PlayerHealthEventChannel _eventChannel;

    [Tooltip("Response to receiving signal from Event Channel")]
    [SerializeField] private UnityEvent<PlayerHealthEventChannel.PlayerHealthEvent> _response;

    private void OnEnable()
    {
        if (_eventChannel != null)
        {
            _eventChannel.OnEventRaised += OnEventRaised;
        }
    }

    private void OnDisable()
    {
        if (_eventChannel != null)
        {
            _eventChannel.OnEventRaised -= OnEventRaised;
        }
    }

    public void OnEventRaised(PlayerHealthEventChannel.PlayerHealthEvent hpEvent)
    {
        _response?.Invoke(hpEvent);
    }
}
