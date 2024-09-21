using UnityEngine;

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

    public void StopTime(PlayerDeathEvent deathEvent)
    {
        Time.timeScale = 0;
    }
}
