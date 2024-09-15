using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    [SerializeField] protected string _textKey;
    [SerializeField] protected float _textSpeed;

    protected InputAction _interactInput;
    protected bool _isPlayerNearby;
    protected bool _messageShown;

    protected virtual void Awake()
    {
        _isPlayerNearby = false;
        _interactInput = new PlayerInput().Player.Interact;
    }

    protected virtual void OnEnable()
    {
        _interactInput.Enable();
    }

    protected virtual void OnDisable()
    {
        _interactInput.Disable();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            _isPlayerNearby = true;
            _messageShown = false;
        }
    }

    protected virtual void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            _isPlayerNearby = false;
            DialogManager.Instance.ShowMessageBox(false);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            _isPlayerNearby = true;
            _messageShown = false;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            _isPlayerNearby = false;
            DialogManager.Instance.ShowMessageBox(false);
        }
    }

    protected virtual void Update()
    {
        if (IsPlayerInteracting() && _textKey != null && _textKey != "" && !_messageShown)
        {
            _messageShown = true;
            DialogManager.Instance.ShowMessage(TextManager.GetText(_textKey), _textSpeed);
        }
    }

    protected bool IsPlayerInteracting()
    {
        return _isPlayerNearby && _interactInput.WasPressedThisFrame();
    }
}
