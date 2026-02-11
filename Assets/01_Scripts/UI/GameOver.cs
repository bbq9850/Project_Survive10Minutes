using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private GameObject gameOverPanel;

    private void Start()
    {
        playerHealth.OnDead += HandleGameOver;
        gameOverPanel.SetActive(false);
    }

    private void HandleGameOver()
    {
        Time.timeScale = 0f;   
        gameOverPanel.SetActive(true);
    }

    private void OnDestroy()
    {
        playerHealth.OnDead -= HandleGameOver;
    }
}
