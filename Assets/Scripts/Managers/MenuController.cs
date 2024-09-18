using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour
{
    public RectTransform marker;
    public TextMeshProUGUI[] menuOptions;
    private int currentIndex = 0;

    public GameObject mainMenuPanel;
    public GameObject volumeSettingsPanel;

    public AudioSource sfxSource;
    public AudioClip moveSound;
    public AudioClip selectSound;

    void Start()
    {
        UpdateMarkerPosition();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentIndex--;
            if (currentIndex < 0)
            {
                currentIndex = menuOptions.Length - 1;
            }
            UpdateMarkerPosition();
            PlayMoveSound();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentIndex++;
            if (currentIndex >= menuOptions.Length)
            {
                currentIndex = 0;
            }
            UpdateMarkerPosition();
            PlayMoveSound();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SelectOption();
            PlaySelectSound();
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

    void SelectOption()
    {
        if (currentIndex == 0)
        {
            Debug.Log("Start Game selected");
            // Add logic to start the game here
        }
        else if (currentIndex == 1) 
        {
            Debug.Log("Volume Settings selected");
            ShowVolumeSettings();
        }
        else if (currentIndex == 2)
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
}
