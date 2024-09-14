using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance { get; private set; }

    private TextMeshProUGUI _textMeshPro;
    private InputAction _cancelInput;

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
        _textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
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
        if (gameObject.activeSelf && _cancelInput.WasPressedThisFrame())
        {
            gameObject.SetActive(false);
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
            yield return new WaitForSeconds(speed);
        }
    }
}
