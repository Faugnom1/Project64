using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ChoiceMessageManager : MonoBehaviour
{
    public static ChoiceMessageManager Instance { get; private set; }

    [Header("Message Boxes")]
    [SerializeField] private GameObject _choiceMessageBox;

    public UnityEvent<GameObject> OnYesClick { get; private set; }
    public UnityEvent<GameObject> OnNoClick { get; private set; }
    public UnityEvent<GameObject> OnMessageRead { get; private set; }
    private TextMeshProUGUI _choiceTextMesh;
    private GameObject _leftSelectArrow;
    private GameObject _rightSelectArrow;
    private PlayerInput _playerInput;
    private GameObject _currentEventObject;
    private bool _playerIsChoosing;
    private bool _messageDisplayed;

    private void Awake()
    {
        OnYesClick = new UnityEvent<GameObject>();
        OnNoClick = new UnityEvent<GameObject>();
        OnMessageRead = new UnityEvent<GameObject>();
        _playerInput = new PlayerInput();

        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _choiceTextMesh = _choiceMessageBox.GetComponentInChildren<TextMeshProUGUI>();
        _leftSelectArrow = _choiceMessageBox.transform.Find("YesText/LeftSelectionArrow").gameObject;
        _rightSelectArrow = _choiceMessageBox.transform.Find("NoText/RightSelectionArrow").gameObject;

        EnableLeftArrow();
    }

    private void OnEnable()
    {
        _playerInput.Player.Move.Enable();
        _playerInput.Player.Interact.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Player.Move.Disable();
        _playerInput.Player.Interact.Disable();
    }

    private void Update()
    {
        // Do nothing if no message boxes on screen
        if (!_choiceMessageBox.activeSelf)
        {
            return;
        }

        if (_playerIsChoosing)
        {
            GameManager.Instance.PlayerMovement.DisableMovement();
            Vector2 moveVector = _playerInput.Player.Move.ReadValue<Vector2>();
            bool wasMovePressed = _playerInput.Player.Move.WasPressedThisFrame();
            bool wasInteractPressed = _playerInput.Player.Interact.WasPressedThisFrame();

            if (_messageDisplayed && wasMovePressed)
            {
                if (moveVector.x > 0)
                {
                    EnableRightArrow();
                }
                else if (moveVector.x < 0)
                {
                    EnableLeftArrow();
                }
            }
            else if (_messageDisplayed && wasInteractPressed)
            {
                if (_leftSelectArrow.activeSelf)
                {
                    ShowMessageBox(false);
                    OnYesClick.Invoke(_currentEventObject);
                }
                else if (_rightSelectArrow.activeSelf)
                {
                    ShowMessageBox(false);
                    OnNoClick.Invoke(_currentEventObject);
                }
            }
        }
    }

    private void EnableLeftArrow()
    {
        _leftSelectArrow.SetActive(true);
        _rightSelectArrow.SetActive(false);
    }

    private void EnableRightArrow()
    {
        _leftSelectArrow.SetActive(false);
        _rightSelectArrow.SetActive(true);
    }

    public void ShowMessageBox(bool active)
    {
        _choiceMessageBox.SetActive(active);
        _playerIsChoosing = active;

        if (!active)
        {
            GameManager.Instance.PlayerMovement.EnableMovement();
        }
    }

    public void ShowMessage(string question, float speed = 0.05f, GameObject eventObject = null)
    {
        _currentEventObject = eventObject;
        EnableLeftArrow();

        ShowMessageBox(true);
        StartCoroutine(TypeText(question, speed));
    }

    private IEnumerator TypeText(string question, float speed)
    {
        _messageDisplayed = false;

        for (int i = 0; i < question.Length; i++)
        {
            _choiceTextMesh.SetText(question[..(i + 1)]);
            yield return new WaitForSeconds(speed);
        }

        _messageDisplayed = true;
    }
}
