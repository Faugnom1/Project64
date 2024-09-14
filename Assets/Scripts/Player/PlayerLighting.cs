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

        // Do not set cutoff if they can see around the right corner
        if (!IsRightCornerFree())
        {
            // Calculate positive x cutoff
            RaycastHit2D positiveXHit = Physics2D.Raycast(transform.position, Vector2.right, darkness.extents.x, _collisionMask);
            if (positiveXHit.collider != null)
            {
                // Calculate the ratio of hit to the extents
                cutoff.x = positiveXHit.distance / darkness.size.x + 0.5f;
                Debug.DrawLine(transform.position, (Vector2)transform.position + positiveXHit.distance * Vector2.right, Color.green, Time.deltaTime);
            }
        }

        // Do not set cutoff if they can see around the top corner
        if (!IsTopCornerFree())
        {
            // Calculate positive y cutoff
            RaycastHit2D positiveYHit = Physics2D.Raycast(transform.position, Vector2.up, darkness.extents.y, _collisionMask);
            if (positiveYHit.collider != null)
            {
                // Calculate the ratio of hit to the extents
                cutoff.y = positiveYHit.distance / darkness.size.y + 0.5f;
                Debug.DrawLine(transform.position, (Vector2)transform.position + positiveYHit.distance * Vector2.up, Color.green, Time.deltaTime);
            }
        }

        // Set the cutoff
        _darknessController.SetPositiveCutoff(cutoff);
    }

    private void SetNegativeCutoff(Bounds darkness)
    {
        // Default cutoff is (0 0)
        Vector2 cutoff = Vector2.zero;

        // Do not set cutoff if they can see around the bottom corner
        if (!IsLeftCornerFree())
        {
            // Calculate the negative x cutoff
            RaycastHit2D negativeXHit = Physics2D.Raycast(transform.position, Vector2.left, darkness.extents.x, _collisionMask);
            if (negativeXHit.collider != null)
            {
                // Calculate the ratio of the hit to the extents
                cutoff.x = 1 - (negativeXHit.distance / darkness.size.x + 0.5f);
                Debug.DrawLine(transform.position, (Vector2)transform.position + negativeXHit.distance * Vector2.left, Color.green, Time.deltaTime);
            }
        }

        // Do not set cutoff if they can see around the bottom corner
        if (!IsBottomCornerFree())
        {
            // Calculate the negative y cutoff
            RaycastHit2D negativeYHit = Physics2D.Raycast(transform.position, Vector2.down, darkness.extents.x, _collisionMask);
            if (negativeYHit.collider != null)
            {
                // Calculate the ratio of the hit to the extents
                cutoff.y = 1 - (negativeYHit.distance / darkness.size.y + 0.5f);
                Debug.DrawLine(transform.position, (Vector2)transform.position + negativeYHit.distance * Vector2.down, Color.green, Time.deltaTime);
            }
        }

        // Set the cutoff
        _darknessController.SetNegativeCutoff(cutoff);
    }

    private bool IsTopCornerFree()
    {
        bool rightCornerFree = true;
        bool leftCornerFree = true;

        // Check the top right corner
        Vector2 topRightCheck = transform.position + transform.localScale / 2;
        RaycastHit2D topRightHit = Physics2D.Raycast(topRightCheck, Vector2.up, transform.localScale.x, _collisionMask);
        if (topRightHit.collider != null)
        {
            Debug.DrawRay(topRightCheck, Vector2.up, Color.green, Time.deltaTime);
            rightCornerFree = false;
        }

        // Check the top left corner
        Vector2 topLeftCheck = new Vector2(topRightCheck.x - transform.localScale.x, topRightCheck.y);
        RaycastHit2D topLeftHit = Physics2D.Raycast(topLeftCheck, Vector2.up, transform.localScale.x, _collisionMask);
        if (topLeftHit.collider != null)
        {
            Debug.DrawRay(topLeftCheck, Vector2.up, Color.green, Time.deltaTime);
            leftCornerFree = false;
        }

        // Return true if at least one corner is free
        return rightCornerFree || leftCornerFree;
    }

    private bool IsRightCornerFree()
    {
        bool topCornerFree = true;
        bool bottomCornerFree = true;

        // Check the top right corner
        Vector2 topRightCheck = transform.position + transform.localScale / 2;
        RaycastHit2D topRightHit = Physics2D.Raycast(topRightCheck, Vector2.right, transform.localScale.x, _collisionMask);
        if (topRightHit.collider != null)
        {
            Debug.DrawRay(topRightCheck, Vector2.right, Color.green, Time.deltaTime);
            topCornerFree = false;
        }

        // Check the bottom right corner
        Vector2 bottomRightCheck = new Vector2(topRightCheck.x, topRightCheck.y - transform.localScale.y);
        RaycastHit2D bottomRightHit = Physics2D.Raycast(bottomRightCheck, Vector2.right, transform.localScale.x, _collisionMask);
        if (bottomRightHit.collider != null)
        {
            Debug.DrawRay(bottomRightCheck, Vector2.right, Color.green, Time.deltaTime);
            bottomCornerFree = false;
        }

        // Return true if at least one corner is free
        return topCornerFree || bottomCornerFree;
    }

    private bool IsBottomCornerFree()
    {
        bool rightCornerFree = true;
        bool leftCornerFree = true;

        // Check the bottom left corner
        Vector2 bottomLeftCheck = transform.position - transform.localScale / 2;
        RaycastHit2D bottomLeftHit = Physics2D.Raycast(bottomLeftCheck, Vector2.down, transform.localScale.y, _collisionMask);
        if (bottomLeftHit.collider != null)
        {
            Debug.DrawRay(bottomLeftCheck, Vector2.down, Color.green, Time.deltaTime);
            leftCornerFree = false;
        }

        // Check the bottom right corner
        Vector2 bottomRightCheck = new Vector2(bottomLeftCheck.x + transform.localScale.x, bottomLeftCheck.y);
        RaycastHit2D bottomRightHit = Physics2D.Raycast(bottomRightCheck, Vector2.down, transform.localScale.y, _collisionMask);
        if (bottomRightHit.collider != null)
        {
            Debug.DrawRay(bottomRightCheck, Vector2.down, Color.green, Time.deltaTime);
            rightCornerFree = false;
        }

        // Return true if at least one corner is free
        return leftCornerFree || rightCornerFree;
    }

    private bool IsLeftCornerFree()
    {
        bool topCornerFree = true;
        bool bottomCornerFree = true;

        // Check the bottom left corner
        Vector2 bottomLeftCheck = transform.position - transform.localScale / 2;
        RaycastHit2D bottomLeftHit = Physics2D.Raycast(bottomLeftCheck, Vector2.left, transform.localScale.x, _collisionMask);
        if (bottomLeftHit.collider != null)
        {
            Debug.DrawRay(bottomLeftCheck, Vector2.left, Color.green, Time.deltaTime);
            bottomCornerFree = false;
        }

        // Check the top left corner
        Vector2 topLeftCheck = new Vector2(bottomLeftCheck.x, bottomLeftCheck.y + transform.localScale.y);
        RaycastHit2D topLeftHit = Physics2D.Raycast(topLeftCheck, Vector2.left, transform.localScale.x, _collisionMask);
        if (topLeftHit.collider != null)
        {
            Debug.DrawRay(topLeftCheck, Vector2.left, Color.green, Time.deltaTime);
            topCornerFree = false;
        }

        // Return true if at least one corner is free
        return topCornerFree || bottomCornerFree;
    }
}
