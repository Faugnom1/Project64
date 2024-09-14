using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    [SerializeField] private string _textKey;
    [SerializeField] private float _textSpeed;

    private InputAction _interactInput;
    private bool _isPlayerNearby;

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
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            _isPlayerNearby = false;
        }
    }

    private void Update()
    {
        if (_isPlayerNearby && _interactInput.WasPressedThisFrame())
        {
            DialogManager.Instance.ShowMessage(TextManager.GetText(_textKey), _textSpeed);
        }
    }
}
