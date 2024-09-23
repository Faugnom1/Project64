using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
public class StalkerAnimation : MonoBehaviour
{
    private Animator _animator;
    private NavMeshAgent _agent;

    private readonly Vector2[] CARDINAL_DIRECTIONS = new Vector2[]
    {
        Vector2.up,
        Vector2.down,
        Vector2.left,
        Vector2.right,
    };

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Vector2 cardinalDir = ClosestCardinalDirection();

        if (cardinalDir == Vector2.left)
        {
            StartLeftAnimation();
        }
        else if (cardinalDir == Vector2.right)
        {
            StartRightAnimation();
        }
        else if (cardinalDir == Vector2.up)
        {
            StartUpAnimation();
        }
        else if (cardinalDir == Vector2.down)
        {
            StartDownAnimation();
        }
        else
        {
            StartIdleAnimation();
        }
    }

    private Vector2 ClosestCardinalDirection()
    {
        Vector2 velocity = _agent.velocity;
        if (velocity == Vector2.zero)
        {
            return Vector2.zero;
        }

        Vector2 direction = velocity.normalized;

        float maxDot = -Mathf.Infinity;
        Vector2 closestDirecton = Vector2.zero;
        foreach (Vector2 cardinalDir in CARDINAL_DIRECTIONS)
        {
            float dot = Vector2.Dot(direction, cardinalDir);
            if (dot > maxDot)
            {
                maxDot = dot;
                closestDirecton = cardinalDir;
            }
        }

        return closestDirecton;
    }

    public void StartRightAnimation()
    {
        _animator.SetTrigger("WalkHorizontal");
        transform.localScale = new Vector3(1, 1, 1);
    }

    public void StartLeftAnimation()
    {
        _animator.SetTrigger("WalkHorizontal");
        transform.localScale = new Vector3(-1, 1, 1);
    }

    public void StartDownAnimation()
    {
        _animator.SetTrigger("WalkDown");
    }

    public void StartUpAnimation()
    {
        _animator.SetTrigger("WalkUp");
    }

    public void StartIdleAnimation()
    {
        _animator.SetTrigger("Idle");
    }
}
