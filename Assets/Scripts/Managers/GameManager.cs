using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public PlayerInventory PlayerInventory { get; private set; }
    public PlayerMovement PlayerMovement { get; private set; }

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
