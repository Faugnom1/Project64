using TMPro;
using UnityEngine;

public class DialogBox : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    private TextMeshProUGUI _textMeshPro;
    private PlayerInteract _playerInteract;
    private PlayerInput _playerInput;

    private void Awake()
    {
         _playerInput = new PlayerInput();
    }

    private void Start()
    {
        gameObject.SetActive(false);

        _textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        _playerInteract = _player.GetComponent<PlayerInteract>();

        _playerInteract.DialogEvent.AddListener(SetMessage);
    }

    private void OnEnable()
    {
        _playerInput.UI.Cancel.Enable();
        gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        _playerInput.UI.Cancel.Disable();
    }

    private void SetMessage(string message)
    {
        gameObject.SetActive(true);
        _textMeshPro.SetText(message);
    }

    private void Update()
    {
        bool wasCancelPressed = _playerInput.UI.Cancel.ReadValue<float>() > 0;

        if (gameObject.activeSelf && wasCancelPressed)
        {
            gameObject.SetActive(false);
        }
    }
}
