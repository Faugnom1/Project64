using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public interface IPlayerAttackable
{
    void Immobilize();
    void Release(Vector2 fromPosition, Action releaseCallback);
    void Bleed();
}

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerAttackable : MonoBehaviour, IPlayerAttackable
{
    [Header("Event Channels")]
    [SerializeField] private PlayerHealthEventChannel _healthEvent;
    [SerializeField] private PlayerDeathEventChannel _deathEvent;

    [SerializeField] private float _releaseForce;
    [SerializeField] private float _lightFlickerInterval;
    [SerializeField] private int _maxFlicker;

    [SerializeField] private float _maxHP;
    [SerializeField] private float _enemyAttackDamage;

    private Light2D _light;
    private Rigidbody2D _rb;
    private PlayerMovement _movement;
    private ParticleSystem _bleedParticles;

    private bool _releaseOnStop;
    private Action _releaseAction;

    private const float RELEASE_VELOCITY_THRESH = 0.1f;
    private const float FLICKER_RANDOM_RANGE = 0.2f;

    private bool _isImmobilized;

    private float _currentHP;

    private void Awake()
    {
        _currentHP = _maxHP;
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _movement = GetComponent<PlayerMovement>();
        _bleedParticles = GetComponentInChildren<ParticleSystem>();
        _light = GetComponentInChildren<Light2D>();
    }
    private void FixedUpdate()
    {
        if (_releaseOnStop)
        {
            CheckRelease();
        }
    }

    public void Immobilize()
    {
        _movement.DisableMovement();
        _isImmobilized = true;
    }

    public void Release(Vector2 fromPosition, Action releaseCallback)
    {
        Vector2 releaseDirection = ((Vector2)transform.position - fromPosition).normalized;
        _rb.AddForce(releaseDirection * _releaseForce, ForceMode2D.Impulse);
        _releaseOnStop = true;
        _releaseAction = releaseCallback;
        _isImmobilized = false;

        DoAttackDamage();
    }

    public void Bleed()
    {
        _bleedParticles.Play();
        StartCoroutine(FlickerRoutine());
    }

    private void CheckRelease()
    {
        if (Mathf.Abs(_rb.velocity.x) < RELEASE_VELOCITY_THRESH && 
            Mathf.Abs(_rb.velocity.y) < RELEASE_VELOCITY_THRESH)
        {
            _releaseOnStop = false;
            _movement.EnableMovement();
            if (_releaseAction != null)
            {
                _releaseAction();
            }
            if (_currentHP == 0)
            {
                _deathEvent.RaiseEvent(new PlayerDeathEvent());
            }
        }
    }

    private IEnumerator FlickerRoutine()
    {
        int flickerNum = 0;
        while (_isImmobilized && flickerNum < _maxFlicker)
        {
            flickerNum++;
            float interval = UnityEngine.Random.Range(
                _lightFlickerInterval - FLICKER_RANDOM_RANGE,
                _lightFlickerInterval + FLICKER_RANDOM_RANGE);
            yield return new WaitForSeconds(interval);
            yield return DoFlicker();
        }
    }

    private IEnumerator DoFlicker()
    {
        yield return SetLight(false, 0.02f);
        yield return SetLight(true, 0.04f);
        yield return SetLight(false, 0.03f);
        yield return SetLight(true, 0.03f);
        yield return SetLight(false, 0.01f);

        // Always end enabled
        _light.enabled = true;
    }

    private IEnumerator SetLight(bool enabled, float duration)
    {
        _light.enabled = enabled;
        yield return new WaitForSeconds(duration);
    }

    private void DoAttackDamage()
    {
        _currentHP -= _enemyAttackDamage;
        if (_currentHP < 0)
        {
            _currentHP = 0;
        }
        _healthEvent.RaiseEvent(new PlayerHealthEvent(_maxHP, _currentHP));
    }
}
