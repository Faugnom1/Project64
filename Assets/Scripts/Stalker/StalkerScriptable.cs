using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StalkerScriptable : MonoBehaviour
{
    public UnityEvent OnStalkerScriptedEventComplete {  get; private set; }

    private StalkerNav _navComponent;

    private void Awake()
    {
        OnStalkerScriptedEventComplete = new UnityEvent();
    }

    private void Start()
    {
        _navComponent = GetComponent<StalkerNav>();
    }

    private void Update()
    {
        
    }

    public void TakeControl()
    {
        //_navComponent.StopNav();
    }

    public void ReturnControl()
    {
        //_navComponent.ResumeNav();
    }

    public void SetPath(Vector2 start, Vector2 end, float speed)
    {
        transform.position = start;
        _navComponent.SetScriptedEventDestination(end, speed);
    }

    public void PathComplete()
    {
        OnStalkerScriptedEventComplete.Invoke();
    }
}
