using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLighting : MonoBehaviour
{
    [SerializeField] private LayerMask _collisionMask;

    private DarknessController _darknessController;

    private readonly Vector2[] CARDINAL_DIRECTIONS = new Vector2[4]
    {
        Vector2.up,
        Vector2.down,
        Vector2.left,
        Vector2.right,
    };

    private void Start()
    {
        _darknessController = GetComponentInChildren<DarknessController>();
    }

    private void Update()
    {
        CalculateLightingCollisions();
    }

    private void CalculateLightingCollisions()
    {
        // Retrieve our current lighting bounds
        Bounds lightingBounds = _darknessController.DarknessBounds;
        SetPositiveCutoff(lightingBounds);
        SetNegativeCutoff(lightingBounds);
    }

    private void SetPositiveCutoff(Bounds darkness)
    {
        // Default cutoff is (1 1)
        Vector2 cutoff = Vector2.one;

        // Calculate positive x cutoff
        RaycastHit2D positiveXHit = Physics2D.Raycast(transform.position, Vector2.right, darkness.extents.x, _collisionMask);
        if (positiveXHit.collider != null)
        {
            // Calculate the ratio of hit to the extents
            cutoff.x = positiveXHit.distance / darkness.size.x + 0.5f;
            Debug.DrawLine(transform.position, (Vector2)transform.position + positiveXHit.distance * Vector2.right, Color.green, Time.deltaTime);
        }

        // Calculate positive y cutoff
        RaycastHit2D positiveYHit = Physics2D.Raycast(transform.position, Vector2.up, darkness.extents.y, _collisionMask);
        if (positiveYHit.collider != null)
        {
            // Calculate the ratio of hit to the extents
            cutoff.y = positiveYHit.distance / darkness.size.y + 0.5f;
            Debug.DrawLine(transform.position, (Vector2)transform.position + positiveYHit.distance * Vector2.up, Color.green, Time.deltaTime);
        }

        // Set the cutoff
        _darknessController.SetPositiveCutoff(cutoff);
    }

    private void SetNegativeCutoff(Bounds darkness)
    {
        // Default cutoff is (0 0)
        Vector2 cutoff = Vector2.zero;

        // Calculate the negative x cutoff
        RaycastHit2D negativeXHit = Physics2D.Raycast(transform.position, Vector2.left, darkness.extents.x, _collisionMask);
        if (negativeXHit.collider != null)
        {
            // Calculate the ratio of the hit to the extents
            cutoff.x = 1 - (negativeXHit.distance / darkness.size.x + 0.5f);
            Debug.DrawLine(transform.position, (Vector2)transform.position + negativeXHit.distance * Vector2.left, Color.green, Time.deltaTime);
        }

        // Calculate the negative y cutoff
        RaycastHit2D negativeYHit = Physics2D.Raycast(transform.position, Vector2.down, darkness.extents.x, _collisionMask);
        if (negativeYHit.collider != null)
        {
            // Calculate the ratio of the hit to the extents
            cutoff.y = 1 - (negativeYHit.distance / darkness.size.y + 0.5f);
            Debug.DrawLine(transform.position, (Vector2)transform.position + negativeYHit.distance * Vector2.down, Color.green, Time.deltaTime);
        }

        // Set the cutoff
        _darknessController.SetNegativeCutoff(cutoff);
    }
}
