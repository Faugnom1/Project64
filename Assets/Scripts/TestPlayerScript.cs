using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerScript : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private PlayerInput _playerInput;
    private Animator _animator;
    private Vector2 _moveInput;
    private Rigidbody2D _rigidBody;
    private Vector2 _facingDirection;

    private void Awake()
    {
        _playerInput = new PlayerInput();
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _playerInput.Player.Move.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Player.Move.Disable();
    }

    private void Update()
    {
        _moveInput = _playerInput.Player.Move.ReadValue<Vector2>();
        bool wasPressedOnce = _playerInput.Player.Move.WasPressedThisFrame();

        if (_moveInput.magnitude == 0)
        {
            IdleAnimation();
        }

        if (wasPressedOnce)
        {
            if (_moveInput.x > 0)
            {
                WalkRightAnimation();
            }
            else if (_moveInput.x < 0)
            {
                WalkLeftAnimation();
            }
            else if (_moveInput.y > 0)
            {
                WalkUpAnimation();
            }
            else if (_moveInput.y < 0)
            {
                WalkDownAnimation();
            }
        }
    }

    private void FixedUpdate()
    {
        _rigidBody.velocity = _moveInput * _moveSpeed;
    }

    private void IdleAnimation()
    {
        _animator.SetBool("Idle", true);
        _animator.SetBool("WalkLeft", false);
        _animator.SetBool("WalkRight", false);
        _animator.SetBool("WalkUp", false);
        _animator.SetBool("WalkDown", false);
    }

    private void WalkLeftAnimation()
    {
        _animator.SetBool("Idle", false);
        _animator.SetBool("WalkLeft", true);
        _animator.SetBool("WalkRight", false);
        _animator.SetBool("WalkUp", false);
        _animator.SetBool("WalkDown", false);
    }

    private void WalkRightAnimation()
    {
        _animator.SetBool("Idle", false);
        _animator.SetBool("WalkLeft", false);
        _animator.SetBool("WalkRight", true);
        _animator.SetBool("WalkUp", false);
        _animator.SetBool("WalkDown", false);
    }

    private void WalkUpAnimation()
    {
        _animator.SetBool("Idle", false);
        _animator.SetBool("WalkLeft", false);
        _animator.SetBool("WalkRight", false);
        _animator.SetBool("WalkUp", true);
        _animator.SetBool("WalkDown", false);
    }

    private void WalkDownAnimation()
    {
        _animator.SetBool("Idle", false);
        _animator.SetBool("WalkLeft", false);
        _animator.SetBool("WalkRight", false);
        _animator.SetBool("WalkUp", false);
        _animator.SetBool("WalkDown", true);
    }
}
