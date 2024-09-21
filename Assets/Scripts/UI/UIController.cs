using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameOverPanelController _gameOverPanel;
    private void Awake()
    {
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    public void ShowGameOver(PlayerDeathEvent deathEvent)
    {
        _gameOverPanel.gameObject.SetActive(true);
    }
}
