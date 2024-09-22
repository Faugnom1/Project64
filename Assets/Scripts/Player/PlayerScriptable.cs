using Cinemachine;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerScriptable : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private Light2D _light2;
    private CinemachineImpulseSource _impulseSource;

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _impulseSource = GetComponent<CinemachineImpulseSource>();
        _light2 = GetComponentInChildren<Light2D>();
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
}
