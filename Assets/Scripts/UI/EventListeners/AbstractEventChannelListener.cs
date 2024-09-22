using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AbstractEventChannelListener<TEventChannel, TEventType> : 
    MonoBehaviour where TEventChannel : AbstractEventChannel<TEventType>
{
    [Header("Listen to Event Channels")]
    [SerializeField] private TEventChannel _eventChannel;

    [Tooltip("Response to receiving signal from Event Channel")]
    [SerializeField] private UnityEvent<TEventType> _response;

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

    public void OnEventRaised(TEventType eventType)
    {
        _response?.Invoke(eventType);
    }
}
