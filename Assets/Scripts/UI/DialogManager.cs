using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance { get; private set; }

    [SerializeField] private GameObject _nextArrow;
    [SerializeField] private GameObject _textMesh;

    private TextMeshProUGUI _textMeshPro;
    private InputAction _cancelInput;
    private InputAction _interactInput;
    private int _currentPage;
    private int _pageCount;

    private void Awake()
    {
        PlayerInput input = new PlayerInput();
        _cancelInput = input.UI.Cancel;
        _interactInput = input.Player.Interact;

        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        gameObject.SetActive(false);
        _currentPage = 1;
        _textMeshPro = _textMesh.GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        _cancelInput.Enable();
        _interactInput.Enable();
    }

    private void OnDisable()
    {
        _cancelInput.Disable();
        _interactInput.Disable();
    }

    private void Update()
    {
        if (!gameObject.activeSelf)
        {
            return;
        }

        // Remove box when cancel button is pressed
        if (_cancelInput.WasPressedThisFrame())
        {
            gameObject.SetActive(false);
        }

        // Show next arrow button and cycle through pages
        if (_pageCount > 1 && _currentPage != _pageCount)
        {
            _nextArrow.SetActive(true);

            if (_interactInput.WasPressedThisFrame())
            {
                _textMeshPro.pageToDisplay = ++_currentPage;
            }
        }
        else
        {
            _nextArrow.SetActive(false);
        }
    }

    public void ShowMessageBox(bool active)
    {
        if (this != null)
        {
            gameObject.SetActive(active);
        }
    }

    public void ShowMessage(object message, float speed = 0.05f)
    {
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
        gameObject.SetActive(true);
        StartCoroutine(TypeText(fullMessage, speed));
    }

    private IEnumerator TypeText(string message, float speed = 0.05f)
    {
        for (int i = 0; i < message.Length; i++)
        {
            _textMeshPro.SetText(message[..(i + 1)]);
            _pageCount = _textMeshPro.textInfo.pageCount;
            yield return new WaitForSeconds(speed);
        }
    }
}
