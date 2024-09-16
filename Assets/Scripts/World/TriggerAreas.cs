using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerAreas : MonoBehaviour
{
    [SerializeField] private ScriptedEventSO _scriptedEvent;

    private BoxCollider2D _collider;

    private void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.CompareTag("Player"))
        {
            _collider.enabled = false;
            _scriptedEvent.StartEvent();
        }
    }
}
