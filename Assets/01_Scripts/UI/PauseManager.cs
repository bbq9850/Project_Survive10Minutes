using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameTimer gameTimer;
    [SerializeField] private GameObject pausePanel;

    private bool isPaused;

    public void Pause()
    {
        Time.timeScale = 0f;
        gameTimer.PauseTime();
        pausePanel.SetActive(true);
        isPaused = true;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        gameTimer.ResumeTime();
        pausePanel.SetActive(false);
        isPaused = false;
    }

    private void Start()
    {
        pausePanel.SetActive(false);
        isPaused = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EscKeyboard();
        }
    }

    void EscKeyboard()
    {
        if (isPaused) Resume();
        else Pause();
    }
}
