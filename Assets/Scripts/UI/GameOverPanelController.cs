using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameOverPanelController : MonoBehaviour
{
    enum GameOverOption
    {
        Restart,
        Exit
    }

    [SerializeField] private TextMeshProUGUI _restartText;
    [SerializeField] private TextMeshProUGUI _exitText;

    [SerializeField] private Color _selectedColor;
    [SerializeField] private Color _baseColor;

    private GameOverOption _currentOption;

    private PlayerInput _input;

    private void Awake()
    {
        _input = new PlayerInput();    
    }

    private void OnEnable()
    {
        _input.UI.Navigate.Enable();
        _input.UI.Navigate.performed += OnNavigate;

        SelectRestart();
    }

    private void OnDisable()
    {
        _input.UI.Navigate.performed -= OnNavigate;
        _input.UI.Navigate.Disable();
    }

    private void SelectRestart()
    {
        _restartText.color = _selectedColor;
        _exitText.color = _baseColor;
        _currentOption = GameOverOption.Restart;
    }

    private void SelectExit()
    {
        _restartText.color = _baseColor;
        _exitText.color = _selectedColor;
        _currentOption = GameOverOption.Exit;
    }

    private void OnNavigate(InputAction.CallbackContext context)
    {
        Vector2 direction = _input.UI.Navigate.ReadValue<Vector2>();
        if (direction == Vector2.right)
        {
            SelectExit();
        }
        else if (direction == Vector2.left)
        {
            SelectRestart();
        }
    }
}
