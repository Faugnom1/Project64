using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(BoxCollider2D), typeof(NavMeshObstacle))]
public class VentObstacle : MonoBehaviour
{
    private NavMeshObstacle _obstacle;
    private BoxCollider2D _collider;

    private void Start()
    {
        _obstacle = GetComponent<NavMeshObstacle>();
        _collider = GetComponent<BoxCollider2D>();
    }

    public void Activate()
    {
        _collider.enabled = true;
        _obstacle.enabled = true;
    }
}
