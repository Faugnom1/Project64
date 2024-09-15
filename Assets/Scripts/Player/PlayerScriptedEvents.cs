using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScriptedEvents : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private PlayerInteraction _playerInteraction;
    private CinemachineImpulseSource _impulseSource;

    [Header("StompTriggerInteraction0")]
    [SerializeField] private float _stompTriggerInteraction0_ShakeForce;
    [SerializeField] private float _stompTriggerInteraction0_Duration;

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerInteraction = GetComponent<PlayerInteraction>();
        _impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    #region StompTriggerInteraction0

    public void StartSequence_StompTriggerInteraction0()
    {
        _playerMovement.DisableMovement();

        // TODO: Kick off monster roaring sound effect

        _impulseSource.GenerateImpulseWithForce(1);
        StartCoroutine(EnableMovement(_stompTriggerInteraction0_Duration));
    }

    #endregion

    private IEnumerator EnableMovement(float wait)
    {
        yield return new WaitForSeconds(wait);
        _playerMovement.EnableMovement();
    }
}
