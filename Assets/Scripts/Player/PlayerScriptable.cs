using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScriptable : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private CinemachineImpulseSource _impulseSource;

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void TakeControl()
    {
        _playerMovement.DisableMovement();
    }

    public void ReturnControl()
    {
        _playerMovement.EnableMovement();
    }

    public void StartScreenShake()
    {
        _impulseSource.GenerateImpulseWithForce(1);
    }
}
