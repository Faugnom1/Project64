using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class Vent : MonoBehaviour
{
    [SerializeField] private UnityEvent _onVentOpen;
    [SerializeField] private AudioClip _openClip;

    private BoxCollider2D _boxCollider;
    private ParticleSystem[] _particles;
    private VentObstacle _ventObstacle;

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _particles = GetComponentsInChildren<ParticleSystem>();
        _ventObstacle = GetComponentInChildren<VentObstacle>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_boxCollider != null)
        {
            _boxCollider.enabled = false;
            _ventObstacle.Activate();
            PlayParticles();
            _onVentOpen.Invoke();
            SoundEffectsManager.Instance.PlaySoundEffect(_openClip, transform.position);
        }
    }

    private void PlayParticles()
    {
        for (int i = 0; i < _particles.Length; i++)
        {
            _particles[i].Play();
        }
    }
}
