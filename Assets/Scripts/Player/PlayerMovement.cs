using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private PlayerInput _playerInput;
    private Rigidbody2D _rb;
    private Animator _animator;

    private Vector2 _moveInput;
    private Vector2 _facingDirection;

    private bool _shouldUpdate;
    private bool _onScriptedPath;

    private Vector2 _scriptedStart;
    private Vector2 _scriptedEnd;
    private float _scriptedTime;
    private float _elapsedScriptedTime;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _shouldUpdate = true;
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
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
            HandleMoveAnimation();
        }
        else
        {
            _moveInput = Vector2.zero;
        }

        // UpdateFacingDirection();
    }

    private void FixedUpdate()
    {
        if (_onScriptedPath)
        {
            if (_elapsedScriptedTime < _scriptedTime)
            {
                Vector2 currentPos = Vector2.Lerp(_scriptedStart, _scriptedEnd, _elapsedScriptedTime / _scriptedTime);
                _rb.MovePosition(currentPos);
                _elapsedScriptedTime += Time.fixedDeltaTime;
            }
            else
            {
                _rb.MovePosition(_scriptedEnd);
                _onScriptedPath = false;
            }
        }
        else if (_shouldUpdate)
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
        _animator.SetTrigger("Idle");
        _rb.velocity = Vector2.zero;
        _shouldUpdate = false;
    }

    public void EnableMovement()
    {
        _shouldUpdate = true;
    }

    public void SetScriptedPath(Vector2 start, Vector2 end, float time)
    {
        _onScriptedPath = true;
        _rb.isKinematic = true;
        _rb.position = start;

        _scriptedStart = start;
        _scriptedEnd = end;
        _scriptedTime = time;
        _elapsedScriptedTime = 0;
    }

    private void HandleMoveAnimation()
    {
        if (_animator != null)
        {
            if (_moveInput == Vector2.zero)
            {
                _animator.SetTrigger("Idle");
            }
            else
            {
                if (_moveInput == Vector2.right || _moveInput == Vector2.left)
                {
                    _animator.SetTrigger("WalkHorizontal");
                    transform.localScale = new Vector3(_moveInput.x, 1, 1);
                }
                else if (_moveInput == Vector2.up)
                {
                    _animator.SetTrigger("WalkUp");
                }
                else if (_moveInput == Vector2.down)
                {
                    _animator.SetTrigger("WalkDown");
                }
            }
        }
    }
}
