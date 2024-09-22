using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseScreenPanel;
    private bool isPaused = false;

    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;

    private Slider[] sliders;

    public RectTransform marker; 
    private int currentSliderIndex = 0;

    public TextMeshProUGUI quitText; 


    public AudioSource sfxSource; 
    public AudioClip moveSound;
    public AudioClip selectSound;

    public float sliderStepSize = 0.05f;

    void Start()
    {
        sliders = new Slider[] { masterVolumeSlider, musicVolumeSlider, sfxVolumeSlider };

        UpdateMarkerPosition();
        isPaused = true;
        masterVolumeSlider.value = VolumeManager.instance.GetMasterVolume();
        musicVolumeSlider.value = VolumeManager.instance.GetMusicVolume();
        sfxVolumeSlider.value = VolumeManager.instance.GetSFXVolume();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }

        if (isPaused)
        {
            HandleNavigationAndVolumeAdjustments();
        }
    }

    public void PauseGame()
    {
        pauseScreenPanel.SetActive(true);
        //Time.timeScale = 0f;  // Freeze the game
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseScreenPanel.SetActive(false);
        //Time.timeScale = 1f;  // Unfreeze the game
        isPaused = false;
    }

    void HandleNavigationAndVolumeAdjustments()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentSliderIndex--;
            if (currentSliderIndex < 0)
            {
                currentSliderIndex = 3;
            }
            UpdateMarkerPosition();
            PlayMoveSound();
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentSliderIndex++;
            if (currentSliderIndex > 3) 
            {
                currentSliderIndex = 0;
            }
            UpdateMarkerPosition();
            PlayMoveSound();
        }

        if (currentSliderIndex < 3)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                sliders[currentSliderIndex].value -= sliderStepSize;
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                sliders[currentSliderIndex].value += sliderStepSize;
            }

            VolumeManager.instance.SetMasterVolume(masterVolumeSlider.value);
            VolumeManager.instance.SetMusicVolume(musicVolumeSlider.value);
            VolumeManager.instance.SetSFXVolume(sfxVolumeSlider.value);
        }

        if (currentSliderIndex == 3 && (Input.GetKeyDown(KeyCode.Return))) 
        {

            QuitGame();
        }
    }

    void UpdateMarkerPosition()
    {
        if (marker != null && masterVolumeSlider != null)
        {
            if (currentSliderIndex == 0)
            {
                marker.position = new Vector3(marker.position.x, masterVolumeSlider.transform.position.y, marker.position.z);
            }
            else if (currentSliderIndex == 1)
            {
                marker.position = new Vector3(marker.position.x, musicVolumeSlider.transform.position.y, marker.position.z);
            }
            else if (currentSliderIndex == 2)
            {
                marker.position = new Vector3(marker.position.x, sfxVolumeSlider.transform.position.y, marker.position.z);
            }
            else if (currentSliderIndex == 3)
            {
                marker.position = new Vector3(marker.position.x, quitText.transform.position.y, marker.position.z);
            }
        }
    }


    void PlayMoveSound()
    {
        if (sfxSource != null && moveSound != null)
        {
            sfxSource.volume = VolumeManager.instance.GetSFXVolume(); 
            sfxSource.PlayOneShot(moveSound);
        }
    }

    void PlaySelectSound()
    {
        if (sfxSource != null && selectSound != null)
        {
            sfxSource.volume = VolumeManager.instance.GetSFXVolume(); 
            sfxSource.PlayOneShot(selectSound);
        }
    }
    void QuitGame()
    {
        Debug.Log("Quit Game selected");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();  // Quit the game in a built application
#endif
    }
}
