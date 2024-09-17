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
    [SerializeField] private float _chaseSpeed;

    private NavMeshAgent _agent;

    private bool _inScriptedEvent;
    private bool _isChasing;

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
        else if (_isChasing)
        {
            HandleChasing();
        }
    }

    private float _hopTimer;

    private void HandleChasing()
    {
        if (_hopTimer >= 0)
        {
            _agent.speed = 3.85f;
            _agent.SetDestination(_target.position);
        }
        else if (_hopTimer < 0)
        {
            _agent.speed = 1.35f;
        }
        if (_hopTimer < -.8f)
        {
            _hopTimer = .4f;
        }
        _hopTimer -= Time.deltaTime;
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

    public void ChasePlayer()
    {
        _isChasing = true;
    }

    public void SnapPosition(Vector2 position)
    {
        _agent.enabled = false;
        transform.position = position;
        _agent.enabled = true;
    }
}
