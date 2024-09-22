using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerAreas : MonoBehaviour
{
    [SerializeField] private ScriptedEventSO _scriptedEvent;
    [SerializeField] private bool _startEnabled;
    [SerializeField] private string _triggerActivatorTag;

    private BoxCollider2D _collider;

    private void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        _collider.enabled = _startEnabled;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.CompareTag(_triggerActivatorTag))
        {
            Debug.Log("Running: " + gameObject.name);
            _collider.enabled = false;
            _scriptedEvent.StartEvent();
        }
    }

    public void EnableTriggerArea()
    {
        _collider.enabled = true;
    }
}
