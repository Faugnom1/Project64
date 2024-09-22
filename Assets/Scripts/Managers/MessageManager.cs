using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public enum MessageType
{
    SMALL,
    LARGE
}

public class MessageManager : MonoBehaviour
{
    public static MessageManager Instance { get; private set; }

    [Header("Message Boxes")]
    [SerializeField] private GameObject _largeMessageBox;
    [SerializeField] private GameObject _smallMessageBox;

    public UnityEvent<GameObject> OnMessageRead { get; private set; }
    private TextMeshProUGUI _largeTextMesh;
    private TextMeshProUGUI _smallTextMesh;
    private GameObject _nextArrow;
    private PlayerInput _playerInput;
    private MessageType _messageType;
    private Coroutine _currentCoroutine;
    private GameObject _currentEventObject;
    private int _currentPage;
    private bool _canGoToNextPage;
    private int _totalPages;
    private string _currentMessage;
    private float _currentSpeed;

    private void Awake()
    {
        OnMessageRead = new UnityEvent<GameObject>();
        _playerInput = new PlayerInput();

        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _largeTextMesh = _largeMessageBox.GetComponentInChildren<TextMeshProUGUI>();
        _smallTextMesh = _smallMessageBox.GetComponentInChildren<TextMeshProUGUI>();
        _nextArrow = _largeMessageBox.transform.Find("NextArrow").gameObject;
    }

    private void OnEnable()
    {
        _playerInput.UI.Cancel.Enable();
        _playerInput.Player.Interact.Enable();
    }

    private void OnDisable()
    {
        _playerInput.UI.Cancel.Disable();
        _playerInput.Player.Interact.Disable();
    }

    private void Update()
    {
        // Do nothing if no message boxes on screen
        if (!_largeMessageBox.activeSelf && !_smallMessageBox.activeSelf)
        {
            return;
        }

        // Check for other logic
        CheckForNextPage();
    }

    private void CheckForNextPage()
    {
        if (_messageType == MessageType.LARGE)
        {
            bool wasInteractPressed = _playerInput.Player.Interact.WasPressedThisFrame();
            _nextArrow.SetActive(_canGoToNextPage);

            if (_currentPage < _totalPages && _canGoToNextPage)
            {
                if (wasInteractPressed)
                {
                    StartCoroutine(TypeText(_currentMessage, _largeTextMesh, _currentSpeed, ++_currentPage));
                }
            }
            else if (_currentPage == _totalPages && _canGoToNextPage && wasInteractPressed)
            {
                _nextArrow.SetActive(false);
                ShowMessageBox(false, _messageType);
                OnMessageRead?.Invoke(_currentEventObject);
            }
        }
        else
        {
            _nextArrow.SetActive(false);
        }
    }

    public void ShowMessageBox(bool active, MessageType type = MessageType.LARGE)
    {
        GameManager.Instance.PlayerMovement.EnableMovement();

        if (this != null && gameObject != null && _largeMessageBox != null)
        {
            // Show correct message box
            if (type == MessageType.LARGE)
            {
                _largeTextMesh.pageToDisplay = 0;
                _largeMessageBox.SetActive(active);
            }
        }
    }

    private string GetFullMessage(object message)
    {
        string fullMessage;

        if (message is List<string> messageList)
        {
            fullMessage = string.Join(" ", messageList);
        }
        else if (message is Dictionary<string, string> messageDict)
        {
            fullMessage = string.Join(" ", messageDict.Values);
        }
        else
        {
            fullMessage = message.ToString();
        }

        return fullMessage;
    }

    public void ShowMessage(object message, MessageType type, float speed = 0.05f, GameObject eventObject = null)
    {
        // Set values
        _playerInput.Player.Interact.Reset();
        _currentPage = 1;
        _totalPages = 0;
        _largeTextMesh.pageToDisplay = 0;
        _messageType = type;
        _currentEventObject = eventObject;
        _currentMessage = GetFullMessage(message);
        _currentSpeed = speed;

        // Show the box and type the message out
        if (type == MessageType.LARGE)
        {
            GameManager.Instance.PlayerMovement.DisableMovement();
            _largeMessageBox.SetActive(true);
            StartCoroutine(TypeText(_currentMessage, _largeTextMesh, _currentSpeed));
        }
        else
        {
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
                _currentCoroutine = null;
            }

            _smallMessageBox.SetActive(true);
            _currentCoroutine = StartCoroutine(TypeText(_currentMessage, _smallTextMesh, _currentSpeed));
        }
    }

    private IEnumerator TypeText(string message, TextMeshProUGUI textMesh, float speed, int page = 1)
    {
        _canGoToNextPage = false;
        textMesh.text = message;
        textMesh.pageToDisplay = page;
        textMesh.ForceMeshUpdate();
        _totalPages = textMesh.textInfo.pageCount;

        TMP_PageInfo pageInfo = textMesh.textInfo.pageInfo[page - 1];
        int startCharIndex = pageInfo.firstCharacterIndex;
        int lastCharIndex = pageInfo.lastCharacterIndex;

        for (int i = startCharIndex; i <= lastCharIndex; i++)
        {
            textMesh.maxVisibleCharacters = i + 1;
            yield return new WaitForSeconds(speed);
        }

        if (_messageType == MessageType.SMALL)
        {
            yield return new WaitForSeconds(2f);
            _smallMessageBox.SetActive(false);
        }

        _canGoToNextPage = true;
    }
}
