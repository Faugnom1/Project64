using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VolumeSettingsController : MonoBehaviour
{
    public RectTransform marker;
    public Slider[] sliders; 
    private int currentSliderIndex = 0;

    public TextMeshProUGUI backText;
    public GameObject volumeSettingsPanel;
    public GameObject mainMenuPanel;

    public AudioSource sfxSource;
    public AudioClip moveSound;
    public AudioClip selectSound;

    public float sliderStepSize = 0.05f;

    void Start()
    {
        UpdateMarkerPosition();

        sliders[0].value = VolumeManager.instance.GetMasterVolume();
        sliders[1].value = VolumeManager.instance.GetMusicVolume();
        sliders[2].value = VolumeManager.instance.GetSFXVolume();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentSliderIndex--;
            if (currentSliderIndex < 0)
            {
                currentSliderIndex = sliders.Length;
            }
            UpdateMarkerPosition();
            PlayMoveSound();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentSliderIndex++;
            if (currentSliderIndex > sliders.Length)
            {
                currentSliderIndex = 0;
            }
            UpdateMarkerPosition();
            PlayMoveSound();
        }

        if (currentSliderIndex < sliders.Length)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                sliders[currentSliderIndex].value -= sliderStepSize;
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                sliders[currentSliderIndex].value += sliderStepSize;
            }

            VolumeManager.instance.SetMasterVolume(sliders[0].value);
            VolumeManager.instance.SetMusicVolume(sliders[1].value);
            VolumeManager.instance.SetSFXVolume(sliders[2].value);
        }
        else if (currentSliderIndex == sliders.Length && Input.GetKeyDown(KeyCode.Return))
        {
            PlaySelectSound();
            volumeSettingsPanel.SetActive(false);
            mainMenuPanel.SetActive(true);
        }
    }

    void UpdateMarkerPosition()
    {
        if (marker != null && sliders != null && sliders.Length > 0)
        {
            if (currentSliderIndex < sliders.Length)
            {
                marker.position = new Vector3(marker.position.x, sliders[currentSliderIndex].transform.position.y, marker.position.z);
            }
            else if (currentSliderIndex == sliders.Length)
            {
                marker.position = new Vector3(marker.position.x, backText.transform.position.y, marker.position.z);
            }
        }
        else
        {
            Debug.LogError("Marker or Sliders not assigned properly in VolumeSettingsController.");
        }
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
