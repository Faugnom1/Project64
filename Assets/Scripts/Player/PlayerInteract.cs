using UnityEngine;
using UnityEngine.Events;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private GameObject _interactBubble;

    private PlayerInput _playerInput;
    public UnityEvent<string> DialogEvent { get; private set; }

    private void Awake()
    {
        _playerInput = new PlayerInput();
        DialogEvent = new UnityEvent<string>();
    }

    private void OnEnable()
    {
        _playerInput.Player.Interact.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Player.Interact.Disable();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Interactable"))
        {
            _interactBubble.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        _interactBubble.SetActive(false);
    }

    private void Update()
    {
        bool wasInteractPressed = _playerInput.Player.Interact.ReadValue<float>() > 0;

        if (_interactBubble.activeSelf && wasInteractPressed)
        {
            DialogEvent.Invoke(TextManager.GetText("book_ending"));
        }
    }
}
