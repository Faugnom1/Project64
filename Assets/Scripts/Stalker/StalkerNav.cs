using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StalkerNav : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private NavMeshAgent _agent;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }
    
    private void Update()
    {
        //_agent.SetDestination(_target.position);
    }

    public void StartSequence()
    {
        Debug.Log("Starting");
    }

    public void SetScriptedEventDestination(Vector2 position)
    {
        _agent.SetDestination(position);
        _agent.speed = 0.3f;
    }
}
