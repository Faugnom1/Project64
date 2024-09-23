using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public PlayerInventory PlayerInventory { get; private set; }
    public PlayerMovement PlayerMovement { get; private set; }

    [SerializeField] private AudioClip _startBGM;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    
    private void Start()
    {
        GameObject player = GameObject.Find("Player");

        PlayerInventory = player.GetComponent<PlayerInventory>();
        PlayerMovement = player.GetComponent<PlayerMovement>();

        BackgroundMusicManager.Instance.ChangeBackgroundMusic(_startBGM);

        int scale = Mathf.FloorToInt(Screen.height / 144f); // Find the closest integer scale factor
        int targetWidth = scale * 160;
        int targetHeight = scale * 144;

        // Set resolution and ensure no stretching
        Screen.SetResolution(targetWidth, targetHeight, FullScreenMode.FullScreenWindow);
    }

    public void ResumeTime()
    {
        Time.timeScale = 1;
    }

    public void StopTime(PlayerDeathEvent deathEvent)
    {
        Time.timeScale = 0;
    }

    public void OnGameOverOptionSelected(UIGameOverOptionEvent gameOverOptionEvent)
    {
        if (gameOverOptionEvent.GameOverOption == GameOverOption.Restart)
        {
            ReloadGame();
        }
    }

    private void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ResumeTime();
    }

    public void EndGame()
    {
        SceneManager.LoadScene("CreditsScene");
    }
}
