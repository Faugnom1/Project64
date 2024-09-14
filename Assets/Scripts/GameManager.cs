using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public PlayerInteraction PlayerInteraction { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        PlayerInteraction = GameObject.Find("Player").GetComponent<PlayerInteraction>();
    }
}
