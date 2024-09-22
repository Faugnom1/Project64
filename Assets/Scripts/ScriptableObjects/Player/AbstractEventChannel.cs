using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class AbstractEventChannel<T> : ScriptableObject
{
    public UnityAction<T> OnEventRaised;

    public void RaiseEvent(T parameter)
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke(parameter);
        }
    }
}
