using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public RectTransform marker;
    public TextMeshProUGUI[] menuOptions;
    private int currentIndex = 0;

    public GameObject mainMenuPanel;
    public GameObject volumeSettingsPanel;
    public GameObject pauseScreen;

    public AudioSource sfxSource;
    public AudioClip moveSound;
    public AudioClip selectSound;

    private PlayerInput _input;

    [SerializeField] private Color _selectedColor;
    [SerializeField] private Color _baseColor;

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
    }

    private void OnDisable()
    {
        _input.UI.Navigate.performed -= OnNavigate;
        _input.UI.Submit.performed -= OnSubmit;

        _input.UI.Navigate.Disable();
        _input.UI.Submit.Disable();
    }

    void Start()
    {
        //UpdateMarkerPosition();
        UpdateSelection();
    }
    
    private void OnNavigate(InputAction.CallbackContext context)
    {
        Vector2 direction = _input.UI.Navigate.ReadValue<Vector2>();
        if (direction == Vector2.up)
        {
            currentIndex--;
            if (currentIndex < 0)
            {
                currentIndex = menuOptions.Length - 1;
            }
            UpdateSelection();
            PlayMoveSound();
        }
        if (direction == Vector2.down)
        {
            currentIndex++;
            if (currentIndex >= menuOptions.Length)
            {
                currentIndex = 0;
            }
            UpdateSelection();
            PlayMoveSound();
        }
    }

    private void OnSubmit(InputAction.CallbackContext context)
    {
        SelectOption();
        PlaySelectSound();
    }

    private void UpdateSelection()
    {
        SetBaseColors();
        TextMeshProUGUI selected = menuOptions[currentIndex];
        if (selected != null)
        {
            selected.color = _selectedColor;
        }
    }

    private void SetBaseColors()
    {
        for (int i = 0; i < menuOptions.Length; i++)
        {
            menuOptions[i].color = _baseColor;
        }
    }

    void UpdateMarkerPosition()
    {
        if (menuOptions != null && menuOptions.Length > 0 && marker != null)
        {
            marker.position = new Vector3(marker.position.x, menuOptions[currentIndex].transform.position.y, marker.position.z);
        }
        else
        {
            Debug.LogError("Menu options or marker not assigned correctly in the inspector.");
        }
    }

    void AccessPauseScreen()
    {
        Debug.Log("Accessing Pause Screen from Start Menu...");
        pauseScreen.SetActive(true);
        mainMenuPanel.SetActive(false);
        Time.timeScale = 0f;
    }

    void SelectOption()
    {
        if (currentIndex == 0)
        {
            Debug.Log("Start Game selected");
            StartGame();
        }
        //else if (currentIndex == 1) 
        //{
        //    Debug.Log("Volume Settings selected");
        //    ShowVolumeSettings();
        //}
        else if (currentIndex == 1)
        {
            Debug.Log("Quit Game selected");
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }

    void ShowVolumeSettings()
    {
        mainMenuPanel.SetActive(false);
        volumeSettingsPanel.SetActive(true);
    }

    void PlayMoveSound()
    {
        if (sfxSource != null && moveSound != null)
        {
            sfxSource.volume = VolumeManager.instance.GetSFXVolume();
            sfxSource.PlayOneShot(moveSound);
        }
        else
        {
            Debug.LogError("SfxSource or MoveSound not assigned.");
        }
    }

    void PlaySelectSound()
    {
        if (sfxSource != null && selectSound != null)
        {
            sfxSource.volume = VolumeManager.instance.GetSFXVolume();
            sfxSource.PlayOneShot(selectSound);
        }
        else
        {
            Debug.LogError("SfxSource or SelectSound not assigned.");
        }
    }

    private void StartGame()
    {
        SceneManager.LoadScene("GroundUp");
    }
}
