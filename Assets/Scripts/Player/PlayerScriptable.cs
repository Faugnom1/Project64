using Cinemachine;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerScriptable : MonoBehaviour
{
    [SerializeField] private RuntimeAnimatorController _playerDarkAnimController;

    private PlayerMovement _playerMovement;
    private Light2D _light2;
    private CinemachineImpulseSource _impulseSource;
    private Animator _animator;
    private RuntimeAnimatorController _originalAnimController;

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _impulseSource = GetComponent<CinemachineImpulseSource>();
        _light2 = GetComponentInChildren<Light2D>();
        _animator = GetComponent<Animator>();
        _originalAnimController = _animator.runtimeAnimatorController;
    }

    public void TakeControl()
    {
        _playerMovement.DisableMovement();
    }

    public void ReturnControl()
    {
        _playerMovement.EnableMovement();
    }

    public void LightsOff()
    {
        _light2.enabled = false;
        TurnPlayerDark();
        StartCoroutine(TurnLightsOn());
    }

    public IEnumerator TurnLightsOn()
    {
        yield return new WaitForSeconds(1);
        _light2.enabled = true;
    }

    public void SetPath(Vector2 start, Vector2 end, float time)
    {
        Vector2 adjustedStart = start;
        if (start == Vector2.zero)
        {
            adjustedStart = transform.position;
        }
        _playerMovement.SetScriptedPath(adjustedStart, end, time);
    }

    public void StartScreenShake()
    {
        _impulseSource.GenerateImpulseWithForce(1);
    }

    public void TurnPlayerLight()
    {
        _animator.runtimeAnimatorController = _originalAnimController;
    }

    public void TurnPlayerDark()
    {
        if (_playerDarkAnimController != null)
        {
            _animator.runtimeAnimatorController = _playerDarkAnimController;
        }
    }
}
