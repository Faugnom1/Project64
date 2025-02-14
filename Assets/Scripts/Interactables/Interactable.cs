using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    [Header("Message Properties")]
    [SerializeField] protected MessageType _messageType;
    [SerializeField] protected float _messageSpeed;
    [SerializeField] protected GameObject _interactBubble;

    protected InputAction _interactInput;
    protected bool _isPlayerNearby;
    protected bool _messageShown;
    protected bool _canInteract;

    protected virtual void Awake()
    {
        _canInteract = true;
        _isPlayerNearby = false;
        _interactInput = new PlayerInput().Player.Interact;
    }

    protected virtual void Start()
    {
        if (_interactBubble != null)
        {
            // Set interact bubble back to original size
            Vector3 parentScale = transform.localScale;
            _interactBubble.transform.localScale = new Vector3(1 / parentScale.x, 1 / parentScale.y, 1 / parentScale.z);
        }
    }

    protected virtual void OnEnable()
    {
        _interactInput.Enable();
    }

    protected virtual void OnDisable()
    {
        _interactInput.Disable();
    }

    protected virtual void OnDestroy()
    {
        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (_canInteract && collision.collider.CompareTag("Player"))
        {
            _isPlayerNearby = true;
            _messageShown = false;
            ToggleInteractBubble(true);
        }
    }

    protected virtual void OnCollisionExit2D(Collision2D collision)
    {
        if (_canInteract && collision.collider.CompareTag("Player"))
        {
            _isPlayerNearby = false;
            ToggleInteractBubble(false);
            MessageManager.Instance.ShowMessageBox(false, _messageType);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (_canInteract && collider.CompareTag("Player"))
        {
            _isPlayerNearby = true;
            _messageShown = false;
            ToggleInteractBubble(true);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collider)
    {
        if (_canInteract && collider.CompareTag("Player"))
        {
            _isPlayerNearby = false;
            ToggleInteractBubble(false);
            MessageManager.Instance.ShowMessageBox(false, _messageType);
        }
    }

    protected virtual void Update()
    {
        if (!_canInteract && _interactBubble.activeSelf)
        {
            _interactBubble.SetActive(false);
        }
    }

    protected virtual void LateUpdate()
    {
        if (_interactBubble != null)
        {
            _interactBubble.transform.rotation = Quaternion.Euler(0, _interactBubble.transform.rotation.eulerAngles.y, 0);
        }
    }

    protected bool IsPlayerInteracting()
    {
        return _canInteract && _isPlayerNearby && _interactInput.WasPressedThisFrame();
    }

    protected void ToggleInteractBubble(bool value)
    {
        if (_interactBubble != null)
        {
            _interactBubble.SetActive(value);
        }
    }
}
