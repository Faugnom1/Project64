using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    [SerializeField] private string _textKey;
    [SerializeField] private float _textSpeed;

    private InputAction _interactInput;
    private bool _isPlayerNearby;
    private bool _messageShown;

    private void Awake()
    {
        _isPlayerNearby = false;
        _interactInput = new PlayerInput().Player.Interact;
    }

    private void OnEnable()
    {
        _interactInput.Enable();
    }

    private void OnDisable()
    {
        _interactInput.Disable();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            _isPlayerNearby = true;
            _messageShown = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            _isPlayerNearby = false;
            DialogManager.Instance.ShowMessageBox(false);
        }
    }

    private void Update()
    {
        if (_isPlayerNearby && !_messageShown && _interactInput.WasPressedThisFrame())
        {
            _messageShown = true;
            DialogManager.Instance.ShowMessage(TextManager.GetText(_textKey), _textSpeed);
        }
    }
}
