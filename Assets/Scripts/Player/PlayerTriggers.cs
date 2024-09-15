using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTriggers : MonoBehaviour
{
    // Events
    [SerializeField] private UnityEvent _onStompTriggerInteraction0;

    private Dictionary<string, UnityEvent> _triggerEvents = new Dictionary<string, UnityEvent>();
    private const string TRIGGER_AREA_ID = "TriggerArea";
    private const string TRIGGER_AREA_StompTrigger_Interaction0 = "StompTrigger_Interaction0";

    private void Start()
    {
        InitializeEvents();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsTriggerArea(collision))
        {
            RunTriggerForName(collision.gameObject.name);
        }
    }

    private void InitializeEvents()
    {
        _triggerEvents.Add(TRIGGER_AREA_StompTrigger_Interaction0, _onStompTriggerInteraction0);
    }

    private bool IsTriggerArea(Collider2D collision)
    {
        return collision != null && collision.CompareTag(TRIGGER_AREA_ID);
    }

    private void RunTriggerForName(string name)
    {
        UnityEvent trigger = _triggerEvents[name];
        if (trigger != null)
        {
            trigger.Invoke();
        }
    }
}
