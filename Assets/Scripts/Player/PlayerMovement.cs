using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private PlayerInput _playerInput;
    private Rigidbody2D _rb;

    private Vector2 _moveInput;
    private Vector2 _facingDirection;

    private bool _shouldUpdate;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _shouldUpdate = true;
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
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
        if (_shouldUpdate)
        {
            _moveInput = _playerInput.Player.Move.ReadValue<Vector2>();
        }
        else
        {
            _moveInput = Vector2.zero;
        }

        UpdateFacingDirection();
    }

    private void FixedUpdate()
    {
        if (_shouldUpdate)
        {
            _rb.velocity = _moveInput * _moveSpeed;
        }
    }

    private void UpdateFacingDirection()
    {
        if (_moveInput != Vector2.zero && _moveInput != _facingDirection)
        {
            _facingDirection = _moveInput;
            
            float angle = Mathf.Atan2(_facingDirection.y, _facingDirection.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90.0f));
            transform.rotation = rotation;
        }
    }

    public void DisableMovement()
    {
        _rb.velocity = Vector2.zero;
        _shouldUpdate = false;
    }

    public void EnableMovement()
    {
        _shouldUpdate = true;
    }
}
