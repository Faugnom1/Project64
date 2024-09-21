using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public PlayerInventory PlayerInventory { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        PlayerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
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
}
