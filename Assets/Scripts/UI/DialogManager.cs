using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance { get; private set; }

    [SerializeField] private GameObject _nextArrow;
    [SerializeField] private GameObject _textMesh;

    private TextMeshProUGUI _textMeshPro;
    private InputAction _cancelInput;
    private int _pages;

    private void Awake()
    {
        _cancelInput = new PlayerInput().UI.Cancel;

        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        gameObject.SetActive(false);
        _textMeshPro = _textMesh.GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        _cancelInput.Enable();
    }

    private void OnDisable()
    {
        _cancelInput.Disable();
    }

    private void Update()
    {
        if (!gameObject.activeSelf)
        {
            return;
        }

        if (_cancelInput.WasPressedThisFrame())
        {
            gameObject.SetActive(false);
        }

        if (_pages > 1)
        {
            _nextArrow.SetActive(true);
        }
        else
        {
            _nextArrow.SetActive(false);
        }
    }

    public void ShowMessageBox(bool active)
    {
        if (gameObject != null && !gameObject.IsDestroyed())
        {
            gameObject.SetActive(active);
        }
    }

    public void ShowMessage(string message, float speed = 0.05f)
    {
        gameObject.SetActive(true);
        StartCoroutine(TypeText(message, speed));
    }

    private IEnumerator TypeText(string message, float speed = 0.05f)
    {
        for (int i = 0; i < message.Length; i++)
        {
            _textMeshPro.SetText(message[..(i + 1)]);
            _pages = _textMeshPro.textInfo.pageCount;
            yield return new WaitForSeconds(speed);
        }
    }
}
