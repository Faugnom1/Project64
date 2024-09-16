using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class StalkerNav : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _stoppingDistanceThreshold;
    [SerializeField] private UnityEvent _onDestinationReached;

    private NavMeshAgent _agent;

    private bool _inScriptedEvent;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }
    
    private void Update()
    {
        if (_inScriptedEvent)
        {
            CheckDestinationReached();
        }
        else
        {
            Debug.Log("Stopped agent");
            _agent.isStopped = true;
        }
    }

    public void SetScriptedEventDestination(Vector2 position, float speed)
    {
        _agent.isStopped = false;
        _inScriptedEvent = true;
        _agent.SetDestination(position);
        _agent.speed = speed;
    }

    public void CheckDestinationReached()
    {
        // Check if the agent has a valid path and its remaining distance is less than the threshold
        if (!_agent.pathPending && _agent.remainingDistance <= _stoppingDistanceThreshold && !_agent.hasPath)
        {
            _inScriptedEvent = false;
            _onDestinationReached.Invoke();
        }
    }
}
