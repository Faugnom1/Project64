using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ScriptedEvent : MonoBehaviour
{
    [SerializeField] private GameObject _eventObject;
    [SerializeField] private Transform _startPosition;
    [SerializeField] private Transform _endPosition;
    [SerializeField] private float _duration;

    private IEventScriptable _eventTarget;

    private void Start()
    {
        _eventTarget = _eventObject.GetComponent<IEventScriptable>();
        if (_eventTarget == null)
        {
            Debug.LogWarning("Event object is not valid: " + _eventObject.name);
        }
    }

    public void StartEventSequence()
    {
        _eventTarget.TakeControl();
        _eventTarget.SetStartPosition(_startPosition.position);
        _eventTarget.SetRotation(_startPosition.rotation);
        _eventTarget.SetEndPosition(_endPosition.position);
    }

    public void StopEventSequence()
    {
        _eventTarget.ReturnControl();
    }
}
