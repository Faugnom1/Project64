using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public enum GameOverOption
{
    Restart,
    Exit
}

public class GameOverPanelController : MonoBehaviour
{
    [SerializeField] private AudioClip _gameOverMusic;
    [SerializeField] private BackgroundMusicManager _backgroundMusic;

    [SerializeField] UIGameOverOptionEventChannel _gameOverOptionEvent;

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
        _input.UI.Submit.Enable();
        _input.UI.Navigate.performed += OnNavigate;
        _input.UI.Submit.performed += OnSubmit;

        SelectRestart();

        _backgroundMusic.ChangeBackgroundMusic(_gameOverMusic);
    }

    private void OnDisable()
    {
        _input.UI.Submit.performed -= OnSubmit;
        _input.UI.Navigate.performed -= OnNavigate;
        _input.UI.Navigate.Disable();
        _input.UI.Submit.Disable();
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

    private void OnSubmit(InputAction.CallbackContext context)
    {
        _gameOverOptionEvent.RaiseEvent(new UIGameOverOptionEvent(_currentOption));
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
