using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewScriptedEventSO", menuName = "ScriptableObjects/ScriptedEventSO")]
public class ScriptedEventSO : ScriptableObject
{
    public bool IsPlayerMovementDisabled;

    public bool ShouldControlPlayerMovement;
    public Vector2 PlayerStart;
    public Vector2 PlayerEnd;
    public float PlayerSpeed;

    public bool ShouldControlStalkerMovement;
    public Vector2 StalkerStart;
    public Vector2 StalkerEnd;
    public float StalkerSpeed;

    public bool ShouldScreenShake;

    [HideInInspector]
    public UnityEvent<ScriptedEventSO> Event;

    private void OnEnable()
    {
        if (Event == null)
        {
            Event = new UnityEvent<ScriptedEventSO>();
        }
    }

    private void OnDisable()
    {
        if (Event != null )
        {
            Event.RemoveAllListeners();
        }
    }

    public void StartEvent()
    {
        Event.Invoke(this);
    }
}
