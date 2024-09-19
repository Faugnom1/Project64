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
    private int _pageCount;
    private bool _messageDisplayed;

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

            if (_pageCount > 1 && _currentPage != _pageCount)
            {
                _nextArrow.SetActive(true);

                if (wasInteractPressed)
                {
                    _largeTextMesh.pageToDisplay = ++_currentPage;
                }
            }

            if (wasInteractPressed && _messageDisplayed && _currentPage == _pageCount)
            {
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
        if (this != null && gameObject != null)
        {
            // Show correct message box
            if (type == MessageType.LARGE)
            {
                _largeMessageBox.SetActive(active);
            }
        }
    }

    public void ShowMessage(object message, MessageType type, float speed = 0.05f, GameObject eventObject = null)
    {
        // Set values
        _currentPage = 1;
        _messageType = type;
        _currentEventObject = eventObject;

        // Combine the messages, if required
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

        // Show the box and type the message out
        if (type == MessageType.LARGE)
        {
            _largeMessageBox.SetActive(true);
            StartCoroutine(TypeText(fullMessage, _largeTextMesh, speed));
        }
        else
        {
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
                _currentCoroutine = null;
            }

            _smallMessageBox.SetActive(true);
            _currentCoroutine = StartCoroutine(TypeText(fullMessage, _smallTextMesh, speed));
        }
    }

    private IEnumerator TypeText(string message, TextMeshProUGUI textMesh, float speed)
    {
        _messageDisplayed = false;

        for (int i = 0; i < message.Length; i++)
        {
            textMesh.SetText(message[..(i + 1)]);
            _pageCount = textMesh.textInfo.pageCount;
            yield return new WaitForSeconds(speed);
        }

        if (_messageType == MessageType.SMALL)
        {
            yield return new WaitForSeconds(2f);
            _smallMessageBox.SetActive(false);
        }

        _messageDisplayed = true;
    }
}
