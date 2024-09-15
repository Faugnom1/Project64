using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAreas : MonoBehaviour
{
    private BoxCollider2D _collider;

    private void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.CompareTag("Player"))
        {
            _collider.enabled = false;
        }
    }
}
