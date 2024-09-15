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

        PlayerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
    }
}
