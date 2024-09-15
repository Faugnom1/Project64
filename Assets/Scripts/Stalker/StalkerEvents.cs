using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkerEvents : MonoBehaviour, IEventScriptable
{
    private StalkerNav _navComponent;

    private void Start()
    {
        _navComponent = GetComponent<StalkerNav>();
    }

    public void TakeControl()
    {
        //_navComponent.StopNav();
    }

    public void ReturnControl()
    {
        //_navComponent.ResumeNav();
    }

    public void SetStartPosition(Vector2 position)
    {
        transform.position = position;
    }

    public void SetRotation(Quaternion rotation)
    {
        transform.rotation = rotation;
    }

    public void SetEndPosition(Vector2 position)
    {
        _navComponent.SetScriptedEventDestination(position);
    }
}
