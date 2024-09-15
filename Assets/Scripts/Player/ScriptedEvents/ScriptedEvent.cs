using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewScriptedEvent", menuName = "ScriptableObjects/ScriptedEvent")]
public class ScriptedEvent : ScriptableObject
{
    public UnityEvent Event;
}
